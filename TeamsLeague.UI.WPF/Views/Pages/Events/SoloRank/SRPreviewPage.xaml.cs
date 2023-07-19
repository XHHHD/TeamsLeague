using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.UI.WPF.Views.Windows.TeamEvents;

namespace TeamsLeague.UI.WPF.Views.Pages.Events.SoloRank
{
    public partial class SRPreviewPage : Page
    {
        private readonly IMemberService _memberService;
        private readonly SoloRankWindow _eventWindow;
        private readonly int _teamId;

        private List<MemberModel> Members { get; set; }
        private List<ToggleButton> ChoosingMembersButtons { get; set; }

        public SRPreviewPage(IMemberService memberService, int teamId, SoloRankWindow eventWindow)
        {
            _memberService = memberService;
            _eventWindow = eventWindow;
            _teamId = teamId;

            Members = new();
            ChoosingMembersButtons = new();

            InitializeComponent();
            BuildComponent();
        }


        private void MemberButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
            {
                if (toggleButton.Tag is MemberModel member)
                {
                    var otherButtons = ChoosingMembersButtons.Where(b => b != toggleButton).ToList();

                    foreach ( var button in otherButtons )
                    {
                        button.IsChecked = false;
                    }

                    _eventWindow.CreateAndShowNextButton(member, true);
                }
            }
        }

        private void MemberButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ChoosingMembersButtons.All(b => b.IsChecked == false))
            {
                _eventWindow.CreateAndShowNextButton(null, false);
            }
        }

        private void BuildComponent()
        {

            Members = _memberService.GetMembersOfTeam(_teamId).ToList();
            if (!Members.Any())
            {
                PotentialAdventurers_StakPanel.Children.Add(new Border
                {
                    Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#10FFFFFF")),
                    BorderThickness = new Thickness(5),
                    Margin = new Thickness(2),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Child = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = "THERE IS NO MEMBERS IN TEAM!",
                        FontFamily = new FontFamily("ArialBlack"),
                        FontSize = 48,
                    }
                });
            }

            foreach (var member in Members)
            {
                var memberButton = BuildMemberButton(member);
                memberButton.Checked += MemberButton_Checked;
                memberButton.Unchecked += MemberButton_Unchecked;

                ChoosingMembersButtons.Add(memberButton);
                PotentialAdventurers_StakPanel.Children.Add(memberButton);
            }
        }

        private ToggleButton BuildMemberButton(MemberModel member)
        {
            ToggleButton toggleButton = new()
            {
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#10FFFFFF")),
                Tag = member,
            };

            Grid grid = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(140) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Image image = new()
            {
                Height = 120,
                Width = 120,
                Source = new BitmapImage(new Uri(ImagesService.GetPositionImageUrl(member.MainPosition), UriKind.Relative)),
            };

            Grid.SetColumn(image, 0);

            Grid innerGrid = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10) });
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition());

            innerGrid.RowDefinitions.Add(new RowDefinition());
            innerGrid.RowDefinitions.Add(new RowDefinition());
            innerGrid.RowDefinitions.Add(new RowDefinition());

            TextBlock textBlock1 = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Attack:",
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock1V = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Text = member.Attack.ToString(),
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock2 = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Defense:",
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock2V = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Text = member.Defense.ToString(),
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock3 = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Intelligence:",
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock3V = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Text = member.Intelligence.ToString(),
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock4 = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Reaction speed:",
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock4V = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Text = member.ReactionSpeed.ToString(),
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock5 = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Mental power:",
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock5V = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Text = member.MentalPower.ToString(),
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock6 = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Mental resistance:",
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            TextBlock textBlock6V = new()
            {
                Margin = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Text = member.MentalResistance.ToString(),
                FontFamily = new FontFamily("ArialBlack"),
                FontSize = 24,
            };

            Grid.SetColumn(textBlock1, 0);
            Grid.SetRow(textBlock1, 0);
            Grid.SetColumn(textBlock1V, 1);
            Grid.SetRow(textBlock1V, 0);
            Grid.SetColumn(textBlock2, 0);
            Grid.SetRow(textBlock2, 1);
            Grid.SetColumn(textBlock2V, 1);
            Grid.SetRow(textBlock2V, 1);
            Grid.SetColumn(textBlock3, 0);
            Grid.SetRow(textBlock3, 2);
            Grid.SetColumn(textBlock3V, 1);
            Grid.SetRow(textBlock3V, 2);
            Grid.SetColumn(textBlock4, 3);
            Grid.SetRow(textBlock4, 0);
            Grid.SetColumn(textBlock4V, 4);
            Grid.SetRow(textBlock4V, 0);
            Grid.SetColumn(textBlock5, 3);
            Grid.SetRow(textBlock5, 1);
            Grid.SetColumn(textBlock5V, 4);
            Grid.SetRow(textBlock5V, 1);
            Grid.SetColumn(textBlock6, 3);
            Grid.SetRow(textBlock6, 2);
            Grid.SetColumn(textBlock6V, 4);
            Grid.SetRow(textBlock6V, 2);


            innerGrid.Children.Add(textBlock1);
            innerGrid.Children.Add(textBlock1V);
            innerGrid.Children.Add(textBlock2);
            innerGrid.Children.Add(textBlock2V);
            innerGrid.Children.Add(textBlock3);
            innerGrid.Children.Add(textBlock3V);
            innerGrid.Children.Add(textBlock4);
            innerGrid.Children.Add(textBlock4V);
            innerGrid.Children.Add(textBlock5);
            innerGrid.Children.Add(textBlock5V);
            innerGrid.Children.Add(textBlock6);
            innerGrid.Children.Add(textBlock6V);

            Grid.SetColumn(innerGrid, 1);

            grid.Children.Add(image);
            grid.Children.Add(innerGrid);

            toggleButton.Content = grid;

            return toggleButton;
        }
    }
}
