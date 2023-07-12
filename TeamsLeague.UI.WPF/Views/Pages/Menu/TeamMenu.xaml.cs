﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.UI.WPF.Buffer;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class TeamMenu : Page
    {
        private readonly ICashBasket _cash;
        private readonly TeamModel _team;


        public TeamMenu(ICashBasket cash, TeamModel team)
        {
            _cash = cash;
            _team = team;
            InitializeComponent();
            BuildComponent();
        }

        private void MemberDetailsButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
        }


        private void BuildComponent()
        {
            TeamName_TextBlock.Text = _team.Name;

            foreach (var member in _team.Members)
            {
                Members_StackPanel.Children.Add(GetMemberViews(member));
            }

            if (_team.Id == _cash.User?.Team.Id)
            {
                Members_StackPanel.Children.Add(new Button
                {
                    MinWidth = 160,
                    Margin = new Thickness(0),
                    Background = null,
                    Foreground = new SolidColorBrush(Color.FromArgb(120, 189, 189, 189)),
                    FontFamily = new FontFamily("Arial Black"),
                    FontSize = 120,
                    Content = "+",
                });
            }
        }

        private Button GetMemberViews(MemberModel member)
        {
            var mainMemberButton = new Button
            {
                MinWidth = 160,
                Background = null,
            };

            mainMemberButton.Click += MemberDetailsButton_Click;

            var stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                Height = 490,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.1,
                },
            };

            var memberName = new TextBlock
            {
                Text = member.Name,
                VerticalAlignment = VerticalAlignment.Top,
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 20,
            };
            stackPanel.Children.Add(memberName);

            var memberLeagueImg = new Image
            {
                MinHeight = 30,
                Margin = new Thickness(3),
                Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-black.png", UriKind.Relative)),
            };
            stackPanel.Children.Add(memberLeagueImg);

            var memberParameter1 = new TextBlock
            {
                Text = "Member stats1",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };
            stackPanel.Children.Add(memberParameter1);

            return mainMemberButton;
        }
    }
}
