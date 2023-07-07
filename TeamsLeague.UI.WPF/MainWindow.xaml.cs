using System.Windows;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.BLL.Services;

namespace TeamsLeague.UI.WPF
{
    public partial class MainWindow : Window
    {
        private IUserServices _userServices = new UserServices();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            var userModel = new UserModel()
            {
                Name = CreateUserName_TextBox.Text,
            };
            userModel = _userServices.CreateUser(userModel);

            UserInfo_Lbael.Content = userModel.Name;
        }
    }
}
