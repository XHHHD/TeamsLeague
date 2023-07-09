using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Windows;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.UI.WPF.Configuration;

namespace TeamsLeague.UI.WPF.Windows
{
    public partial class LobbyWindow : Window
    {
        private readonly IBufferingService _buffer;
        private readonly IUserServices _userServices;
        private readonly ITeamServices _teamServices;
        private const string _createButton = "CREATE!";
        private const string _updateButton = "UPDATE";

        public LobbyWindow(IBufferingService buffer, IUserServices userServices, ITeamServices teamServices)
        {
            InitializeComponent();
            _buffer = buffer;
            _userServices = userServices;
            _teamServices = teamServices;
            _buffer.User = _userServices.GetUsers().FirstOrDefault();
            if (_buffer.User == null)
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
            if (UserName_TextBox.Text == _buffer.User?.Name)
            {
                MessageBox.Show("Try to invent something else for updating!");
            }
            else
            {
                if (_buffer.User == null)
                {
                    _buffer.User = new()
                    {
                        Name = UserName_TextBox.Text,
                    };

                    _buffer.User = _userServices.CreateUser(_buffer.User);
                    _buffer.User = _userServices.AddTeam(UserTeamName_TextBox.Text);
                    _buffer.User = _buffer.User;

                    Optional_Button.Content = _updateButton;
                }
                else
                {
                    _buffer.User.Name = UserName_TextBox.Text;
                    _buffer.User = _userServices.UpdateUser(_buffer.User);

                    if (_buffer.User.Team is not null)
                    {
                        _buffer.User.Team.Name = UserTeamName_TextBox.Text;
                        _buffer.User.Team = _teamServices.UpdateTeam(_buffer.User.Team);
                    }
                }
                GetUserInfo();
            }
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_buffer.User is null || _buffer.User.Team is null)
            {
                MessageBox.Show("First u should register!");
            }
            else
            {
                UnityContainerProvider.GetNew<GameWindow>().Show();
                this.Close();
            }
        }

        private void GetUserInfo()
        {
            UserInfo_Lbael.Content = _buffer.User?.Name + ", " + _buffer.User?.Team?.Name;
            UserName_TextBox.Text = _buffer.User?.Name;
            UserTeamName_TextBox.Text = _buffer.User?.Team?.Name;
        }
    }
}
