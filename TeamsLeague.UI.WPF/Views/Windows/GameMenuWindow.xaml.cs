using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.UI.WPF.Buffer;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Resources.Constants;
using TeamsLeague.UI.WPF.Views.Pages.Menu;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Windows
{
    public partial class GameWindow : Window
    {
        private readonly ICashBasket _cash;
        private readonly List<ToggleButton> _miniBarButtons;


        public GameWindow(ICashBasket cash)
        {
            _cash = cash;
            _miniBarButtons = new();

            InitializeComponent();
            BuildComponent();

            GameMainFrame.Content = UnityContainerProvider.GetNew<TeamMenu>(new ParameterOverride("teamId", _cash.User?.Team.Id));
        }


        private void GameWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckedFullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void UncheckedFullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button)
            {
                if (button.Tag is MenuPagesType type)
                {
                    switch (type)
                    {
                        case MenuPagesType.UserTeamStats:
                            GameMainFrame.Content = UnityContainerProvider.GetNew<TeamMenu>(new ParameterOverride("teamId", _cash.User?.Team.Id));
                            break;

                        case MenuPagesType.LeagueStats:
                            GameMainFrame.Content = UnityContainerProvider.GetNew<LeagueMenu>();
                            break;

                        case MenuPagesType.Trainings:
                            GameMainFrame.Content = UnityContainerProvider.GetNew<TeamTrainingsMenu>();
                            break;

                        case MenuPagesType.TopTeams:
                            break;

                        case MenuPagesType.TopMembers:
                            break;

                        case MenuPagesType.UserStats:
                            GameMainFrame.Content = UnityContainerProvider.GetNew<UserStatsMenu>();
                            break;
                    }

                    button.IsChecked = true;

                    foreach (var otherButton in _miniBarButtons)
                    {
                        if (otherButton != button)
                        {
                            otherButton.IsChecked = false;
                        }
                    }
                }
            }
        }

        private void BuildComponent()
        {
            var toggleFirstButton = true;

            foreach (var type in Enum.GetValues<MenuPagesType>())
            {
                var miniBarMainGrid = new Grid();
                var miniBarButton = new ToggleButton
                {
                    Name = type.ToString() + "MiniBar_Button",
                    Background = null,
                    BorderBrush = null,
                    Height = 120,
                    Width = 240,
                    Content = miniBarMainGrid,
                    Tag = type,
                    IsChecked = toggleFirstButton,
                };

                miniBarButton.Click += MenuButton_Click;

                toggleFirstButton = false;

                PagesMiniBar_StackPanel.Children.Add(miniBarButton);

                GetMiniPage(type, miniBarMainGrid);

                _miniBarButtons.Add(miniBarButton);
            }
        }

        private void GetMiniPage(MenuPagesType pageType, Grid miniBarMainGrid)
        {
            switch (pageType)
            {
                case MenuPagesType.UserTeamStats:
                    ViewUserTeamStats(miniBarMainGrid);
                    break;

                case MenuPagesType.LeagueStats:
                    ViewLeagueStats(miniBarMainGrid);
                    break;

                case MenuPagesType.Trainings:
                    ViewTrainings(miniBarMainGrid);
                    break;

                case MenuPagesType.TopTeams:
                    ViewTopTeams(miniBarMainGrid);
                    break;

                case MenuPagesType.TopMembers:
                    ViewTopMembers(miniBarMainGrid);
                    break;

                case MenuPagesType.UserStats:
                    ViewUserStats(miniBarMainGrid);
                    break;
            }
        }

        private void ViewUserTeamStats(Grid mainGrid)
        {
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            var image = new Image
            {
                Name = "Img",
                Source = _cash.User?.Team?.Image is not null
                ? new BitmapImage(new Uri(_cash.User.Team.Image, UriKind.Relative))
                : new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative)),
                Height = 70,
                Width = 70,
                Margin = new Thickness(3),
            };
            var subText = new TextBlock
            {
                Name = "SubText",
                Text = _cash.User?.Team is not null ? _cash.User.Team.Name.ToUpper() : "Team Name",
                FontSize = 30,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            mainGrid.Children.Add(image);
            mainGrid.Children.Add(subText);
            Grid.SetRow(image, 0);
            Grid.SetRow(subText, 1);
        }

        private void ViewLeagueStats(Grid mainGrid)
        {
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            var image = new Image
            {
                Name = "Img",
                Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative)),
                Height = 70,
                Width = 70,
                Margin = new Thickness(3),
            };
            var subText = new TextBlock
            {
                Name = "SubText",
                Text = "League",
                FontSize = 30,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Grid.SetRow(image, 0);
            Grid.SetRow(subText, 1);
            mainGrid.Children.Add(image);
            mainGrid.Children.Add(subText);
        }

        private void ViewTrainings(Grid mainGrid)
        {
            var topText = new TextBlock
            {
                Name = "TopText",
                Text = _cash.User?.Team is not null ? _cash.User.Team.Name.ToUpper() : "TEAM",
                FontSize = 30,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var subText = new TextBlock
            {
                Name = "SubText",
                Text = "TRAININGS",
                FontSize = 28,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var stackPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, };
            stackPanel.Children.Add(topText);
            stackPanel.Children.Add(subText);
            mainGrid.Children.Add(stackPanel);
        }

        private void ViewTopTeams(Grid mainGrid)
        {
            var topText = new TextBlock
            {
                Name = "TopText",
                Text = "TOP",
                FontSize = 60,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var subText = new TextBlock
            {
                Name = "SubText",
                Text = "TEAMS",
                FontSize = 40,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var stackPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, };
            stackPanel.Children.Add(topText);
            stackPanel.Children.Add(subText);
            mainGrid.Children.Add(stackPanel);
        }

        private void ViewTopMembers(Grid mainGrid)
        {
            var topText = new TextBlock
            {
                Name = "TopText",
                Text = "TOP",
                FontSize = 60,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var subText = new TextBlock
            {
                Name = "SubText",
                Text = "MEMBERS",
                FontSize = 40,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            var stackPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, };
            stackPanel.Children.Add(topText);
            stackPanel.Children.Add(subText);
            mainGrid.Children.Add(stackPanel);
        }

        private void ViewUserStats(Grid mainGrid)
        {
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            var image = new Image
            {
                Name = "UserImg",
                Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-black.png", UriKind.Relative)),
                Height = 70,
                Width = 70,
                Margin = new Thickness(3),
            };
            var subText = new TextBlock
            {
                Name = "SubText",
                Text = "Statistics",
                FontSize = 30,
                FontFamily = new FontFamily("Arial Black"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            mainGrid.Children.Add(image);
            mainGrid.Children.Add(subText);
            Grid.SetRow(image, 0);
            Grid.SetRow(subText, 1);
        }
    }
}