using System.Windows.Controls;
using TeamsLeague.UI.WPF.Views.Windows.TeamEvents;

namespace TeamsLeague.UI.WPF.Views.Pages.Events.SoloRank
{
    public partial class SRResultsPage : Page
    {
        private readonly SoloRankWindow _eventWindow;

        public SRResultsPage(int matchId, SoloRankWindow eventWindow)
        {
            _eventWindow = eventWindow;

            InitializeComponent();

            _eventWindow.CreateAndShowNextButton(null, true);
        }
    }
}
