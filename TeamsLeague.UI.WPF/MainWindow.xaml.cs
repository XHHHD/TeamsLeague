using System.Windows;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Windows;

namespace TeamsLeague.UI.WPF
{
    public partial class MainWindow : Window
    {
        private const string _logoName = "TEAMS LEAGUE";

        public MainWindow()
        {
            InitializeComponent();
            Logo_Label.Content = _logoName;
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            var lobby = UnityContainerProvider.GetNew<LobbyWindow>();

            lobby.Show();
            this.Close();
        }
    }
}
