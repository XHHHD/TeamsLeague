using System.Windows.Controls;
using TeamsLeague.UI.WPF.Buffer;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class TeamTrainingsMenu : Page
    {
        private readonly ICashBasket _cash;

        public TeamTrainingsMenu(ICashBasket cash)
        {
            _cash = cash;
            InitializeComponent();
            BuildComponent();
        }

        private void BuildComponent()
        {
            TeamName_TextBlock.Text = _cash.User?.Team is not null ? _cash.User?.Team.Name : "Team Name";
            TeamShortName_TextBlock.Text = _cash.User?.Team is not null ? _cash.User?.Team.Name.ToUpper().Remove(3) : "ONE";
        }
    }
}
