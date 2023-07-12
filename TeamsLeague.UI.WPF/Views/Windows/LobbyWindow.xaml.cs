using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Windows;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.UI.WPF.Buffer;
using TeamsLeague.UI.WPF.Configuration;

namespace TeamsLeague.UI.WPF.Windows
{
    public partial class LobbyWindow : Window
    {
        private readonly IGameGenerator _gameGenerator;
        private readonly ICashBasket _cash;
        private readonly IUserServices _userService;
        private readonly ITeamService _teamService;
        private readonly ITeamBuilder _teamBuilder;

        private const string _createButton = "CREATE!";
        private const string _updateButton = "UPDATE";

        public LobbyWindow(IGameGenerator gameGenerator, ICashBasket cash, IUserServices userServices, ITeamService teamServices, ITeamBuilder teamBuilder)
        {
            _gameGenerator = gameGenerator;
            _cash = cash;
            _userService = userServices;
            _teamService = teamServices;
            _teamBuilder = teamBuilder;

            _cash.User = _userService.GetAllUsers().FirstOrDefault();
            if ( _cash.User?.Team is not null ) { _cash.User.Team = _teamService.ReadTeam(_cash.User.Team.Id); }

            InitializeComponent();
            if (_cash.User == null)
            {
                Optional_Button.Content = _createButton;
            }
            else
            {
                Optional_Button.Content = _updateButton;
                GetUserInfo();
            }

            _gameGenerator.GenerateEnvironment();
        }

        private void Optional_Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserName_TextBox.Text.IsNullOrEmpty() || UserTeamName_TextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please fill in all fields!");
            }
            else
            if (UserName_TextBox.Text == _cash.User?.Name)
            {
                MessageBox.Show("Try to invent something else for updating!");
            }
            else
            {
                if (_cash.User == null)
                {
                    _cash.User = new()
                    {
                        Name = UserName_TextBox.Text,
                    };

                    _cash.User = _userService.CreateUser(_cash.User);
                    _cash.User.Team = _teamBuilder
                        .GenerateBasicStats(UserTeamName_TextBox.Text)
                        .Build();
                    _cash.User.Team = _teamService.CreateTeam(_cash.User.Team);
                    _cash.User = _userService.UpdateUser(_cash.User);

                    Optional_Button.Content = _updateButton;
                }
                else
                {
                    _cash.User.Name = UserName_TextBox.Text;
                    _cash.User = _userService.UpdateUser(_cash.User);

                    if (_cash.User.Team is not null)
                    {
                        _cash.User.Team.Name = UserTeamName_TextBox.Text;
                        _cash.User.Team = _teamService.UpdateTeam(_cash.User.Team);
                    }
                }
                GetUserInfo();
            }
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_cash.User is null || _cash.User.Team is null)
            {
                MessageBox.Show("First u should register!");
            }
            else
            {
                _cash.GameWindow = UnityContainerProvider.GetNew<GameWindow>();
                _cash.GameWindow.Show();
                this.Close();
            }
        }

        private void GetUserInfo()
        {
            UserInfo_Lbael.Content = _cash.User?.Name + ", " + _cash.User?.Team?.Name;
            UserName_TextBox.Text = _cash.User?.Name;
            UserTeamName_TextBox.Text = _cash.User?.Team?.Name;
        }
    }
}
