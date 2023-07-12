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
        private readonly ICashBasket _cash;
        private readonly IUserServices _userServices;
        private readonly ITeamService _teamServices;
        private const string _createButton = "CREATE!";
        private const string _updateButton = "UPDATE";

        public LobbyWindow(ICashBasket cash, IUserServices userServices, ITeamService teamServices)
        {
            InitializeComponent();
            _cash = cash;
            _userServices = userServices;
            _teamServices = teamServices;
            _cash.User = _userServices.GetUsers().FirstOrDefault();
            if (_cash.User == null)
            {
                Optional_Button.Content = _createButton;
            }
            else
            {
                Optional_Button.Content = _updateButton;
                GetUserInfo();
            }
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

                    _cash.User = _userServices.CreateUser(_cash.User);
                    _cash.User = _userServices.AddTeam(UserTeamName_TextBox.Text);
                    _cash.User = _cash.User;

                    Optional_Button.Content = _updateButton;
                }
                else
                {
                    _cash.User.Name = UserName_TextBox.Text;
                    _cash.User = _userServices.UpdateUser(_cash.User);

                    if (_cash.User.Team is not null)
                    {
                        _cash.User.Team.Name = UserTeamName_TextBox.Text;
                        _cash.User.Team = _teamServices.UpdateTeam(_cash.User.Team);
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
