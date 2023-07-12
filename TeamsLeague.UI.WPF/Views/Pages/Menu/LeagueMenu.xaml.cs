using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.UI.WPF.Buffer;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Windows;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class LeagueMenu : Page
    {
        private readonly ICashBasket _cash;
        private readonly ITeamService _teamService;

        private HashSet<TeamModel> TeamsOfLeague { get; set; }


        public LeagueMenu(ICashBasket cash, ITeamService teamService)
        {
            _cash = cash;
            _teamService = teamService;
            TeamsOfLeague = _teamService.GetTeams().ToHashSet();
            InitializeComponent();
            BuildComponent();
        }


        private void TeamButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is TeamModel team)
                {
                    UnityContainerProvider.GetNew<TeamMenu>(new ParameterOverride("team", _cash.User?.Team));
                }
            }
        }

        private void BuildComponent()
        {
            foreach (var team in TeamsOfLeague)
            {
                TeamsLeague_StackPanel.Children.Add(GetTeamView(team));
            }
        }

        private UIElement GetTeamView(TeamModel team)
        {
            var teamButton = new Button
            {
                Height = 60,
                FontSize = 40,
                Margin = new Thickness(3),
                Background = null,
                Tag = team,
            };

            var teamGrid = new Grid
            {
                Height = 60,
                Width = 930,
            };

            var elementsStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };
            var teamRankImg = new Image
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(2),
                Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-black.png", UriKind.Relative)),
            };

            var teamLogoImg = new Image
            {
                Height = 50,
                Width = 50,
                Margin = new Thickness(2),
                Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative)),
            };
            var teamNameTextBlock = new TextBlock
            {
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 40,
                Text = team.Name,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(3),
            };

            elementsStackPanel.Children.Add(teamLogoImg);
            elementsStackPanel.Children.Add(teamNameTextBlock);

            teamGrid.Children.Add(elementsStackPanel);
            teamGrid.Children.Add(teamRankImg);

            teamButton.Content = teamGrid;
            teamButton.Click += TeamButton_Click;

            return teamButton;
        }
    }
}
