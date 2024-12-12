using System.Windows;
using System.Windows.Controls;
using TeamsLeague.UI.WPF.Buffer;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class UserStatsMenu : Page
    {
        private readonly ICashBasket _cash;

        public UserStatsMenu(ICashBasket cash)
        {
            _cash = cash;
            InitializeComponent();
            BuildComponent();
        }

        private void BuildComponent()
        {
            UserName_TextBlock.Text = _cash.User is not null ? _cash.User.Name : "Adm";
            MatchHistory_StackPanel.Children.Add(new ListBox() { Margin = new Thickness(3) , MinHeight = 50 });
        }
    }
}
