using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Windows;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.BLL.Services;

namespace TeamsLeague.UI.WPF
{
    public partial class MainWindow : Window
    {
        private IUserServices _userServices;
        private UserModel? _userModel;
        private TeamModel? _teamModel;
        private const string _createButton = "CREATE!";
        private const string _updateButton = "UPDATE";

        public MainWindow()
        {
            InitializeComponent();
            _userServices = new UserServices();
            _userModel = _userServices.GetUsers().FirstOrDefault();
            if (_userModel is null )
            {
                Optional_Button.Content = _createButton;
            }
            else
            {
                Optional_Button.Content = _updateButton;

                _teamModel = _userModel.Team;
            }
        }

        private void Optional_Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserName_TextBox.Text.IsNullOrEmpty() || UserTeamName_TextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please fill in all fields!");
            }
            else
            if (UserName_TextBox.Text == _userModel?.Name)
            {
                MessageBox.Show("Try to invent something else!");
            }
            else
            {
                if (_userModel == null)
                {
                    _userModel = new()
                    {
                        Name = UserName_TextBox.Text,
                        Team = new TeamModel()
                        {
                            Name = UserTeamName_TextBox.Text,
                        }
                    };

                    _userModel = _userServices.CreateUser(_userModel);
                    _teamModel = _userModel.Team;

                    Optional_Button.Content = _updateButton;
                }
                else
                {
                    _userModel.Name = UserName_TextBox.Text;
                    //_teamModel.Name = UserTeamName_TextBox.Text;
                    _userModel = _userServices.UpdateUser(_userModel);
                    //_teamModel = _teamService.UpdateTeam(_teamModel);
                }

                UserInfo_Lbael.Content = _userModel.Name;
            }
        }
    }
}
