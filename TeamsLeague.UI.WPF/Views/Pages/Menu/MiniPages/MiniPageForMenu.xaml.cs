using System.Windows.Controls;
using TeamsLeague.UI.WPF.Resources.Constants;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu.MiniPages
{
    public partial class MiniPageForMenu : Page
    {
        private readonly MenuMiniPagesType PageType;

        public MiniPageForMenu(MenuMiniPagesType type)
        {
            InitializeComponent();
            PageType = type;
        }

        private void TButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
