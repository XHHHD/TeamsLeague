using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.UI.WPF.Views.Windows.TeamEvents;

namespace TeamsLeague.UI.WPF.Views.Pages.Events.SoloRank
{
    public partial class SRResultsPage : Page
    {
        private readonly IMatchService _matchService;
        private readonly SoloRankWindow _eventWindow;


        private MatchModel CurrentMatch { get; set; }


        public SRResultsPage(IMatchService matchService, int matchId, SoloRankWindow eventWindow)
        {
            _matchService = matchService;
            _eventWindow = eventWindow;

            CurrentMatch = _matchService.GetMatch(matchId);

            InitializeComponent();
            BuildComponent();

            _eventWindow.CreateAndShowNextButton(null, true);
        }

        private void BuildComponent()
        {
            var logs = CurrentMatch.MatchLog.Split(new[] { "\n" }, StringSplitOptions.None);

            foreach (var log in logs)
            {
                Game_Log.Children.Add(new TextBlock
                {
                    Text = log,
                    FontSize = 24,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(2),
                });

                Thread.Sleep(100);
            }
        }
    }
}
