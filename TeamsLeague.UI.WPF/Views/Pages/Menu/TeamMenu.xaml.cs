using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.UI.WPF.Buffer;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Extensions;
using TeamsLeague.UI.WPF.Views.Windows;
using TeamsLeague.UI.WPF.Views.Windows.Member;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class TeamMenu : Page
    {
        private readonly object _creator;
        private readonly ICashBasket _cash;
        private readonly ITeamService _teamService;
        private readonly IMemberService _memberService;

        private TeamModel Team { get; set; }
        private IEnumerable<MemberModel> Members { get; set; }


        public TeamMenu(ICashBasket cash, ITeamService teamService, IMemberService memberService, int teamId, object creator)
        {
            _creator = creator;
            _cash = cash;
            _teamService = teamService;
            _memberService = memberService;
            Team = _teamService.GetTeam(teamId);
            Members = _memberService.GetMembersOfTeam(teamId);

            InitializeComponent();
            BuildComponent();
        }


        private void MemberDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is MemberModel memberModel)
                {
                    var memberWindow = UnityContainerProvider.GetNew<MemberDetailsWindow>(new ParameterOverride("memberId", memberModel.Id));

                    IsEnabled = false;
                    memberWindow.Closed += (s, args) =>
                    {
                        IsEnabled = true;
                        BuildComponent();
                    };

                    memberWindow.ShowDialog();
                }
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            var potentialPositions = Enum.GetValues<PositionType>().Where(t => !Members.Select(m => m.MainPosition).Contains(t)).ToList();
            var addingMember = UnityContainerProvider.GetNew<AddMemberWindow>(new ParameterOverride("teamId", Team.Id), new ParameterOverride("potentialPositions", potentialPositions));

            IsEnabled = false;
            addingMember.Closed += (s, args) =>
            {
                IsEnabled = true;
                BuildComponent();
            };

            addingMember.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _cash.GameWindow.GameMainFrame.Content = _creator;
        }

        private void BuildComponent()
        {
            Team = _teamService.GetTeam(Team.Id);
            Members = _memberService.GetMembersOfTeam(Team.Id);

            TeamName_TextBlock.Text = Team.Name;
            if (Team.Image is not null)
            {
                TeamLogo.Source = new BitmapImage(new Uri(Team.Image, UriKind.Relative));
            }

            FillInStats();

            Members_StackPanel.Children.Clear();
            foreach (var member in Members)
            {
                Members_StackPanel.Children.Add(GetMemberViews(member, Members_StackPanel.Height));
                Members_StackPanel.Background = null;
            }

            if (Team.Id == _cash.User?.Team.Id)
            {
                BackButton.IsEnabled = false;
                BackButton.Visibility = Visibility.Hidden;

                if (Members.Count() < Enum.GetValues(typeof(PositionType)).Length)
                {
                    var addMemberButton = new Button
                    {
                        Width = 240,
                        Margin = new Thickness(0),
                        Background = null,
                        Foreground = new SolidColorBrush(Color.FromArgb(120, 189, 189, 189)),
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 120,
                        Content = "+",
                    };

                    addMemberButton.Click += AddMemberButton_Click;

                    Members_StackPanel.Children.Add(addMemberButton);
                }
            }
            else
            {
                BackButton.IsEnabled = true;
                BackButton.Visibility = Visibility.Visible;
            }
        }

        private void FillInStats()
        {
            Health_Progress.Value = Team.Health;
            Health_Progress.Maximum = Team.MaxHealth;
            Energy_Progress.Value = Team.Energy;
            Energy_Progress.Maximum = Team.MaxEnergy;
            Honor_TextBlock.Text = Team.Honor.ToString();
            Experience_TextBlock.Text = Team.Experience.ToString();
            RankPoints_TextBlock.Text = Team.RankPoints.ToString();
            Teamplay_TextBlock.Text = Team.Teamplay.ToString();    //IN FEATURE NUMBER WILL BE HIDDEN AND SHOW ONLY MENTHAL LEVEL
        }

        private Button GetMemberViews(MemberModel member, double height)
        {
            #region BUTTON
            var memberButton = new Button
            {
                Height = height,
                MinWidth = 240,
                MaxWidth = 300,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                BorderBrush = Brushes.Transparent,
                VerticalAlignment = VerticalAlignment.Stretch,
                OverridesDefaultStyle = true,
                Tag = member,
            };

            Style buttonStyle = new(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Transparent));
            buttonStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Black));

            ControlTemplate buttonTemplate = new(typeof(Button));
            FrameworkElementFactory borderFactory = new(typeof(Border));
            borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Control.BackgroundProperty));
            borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Control.BorderBrushProperty));
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1));

            FrameworkElementFactory contentPresenterFactory = new(typeof(ContentPresenter));
            contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

            borderFactory.AppendChild(contentPresenterFactory);

            buttonTemplate.VisualTree = borderFactory;
            buttonStyle.Setters.Add(new Setter(Control.TemplateProperty, buttonTemplate));

            Trigger buttonTrigger = new();
            buttonTrigger.Property = UIElement.IsMouseOverProperty;
            buttonTrigger.Value = true;
            buttonTrigger.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Transparent));

            buttonStyle.Triggers.Add(buttonTrigger);

            memberButton.Style = buttonStyle;

            memberButton.Click += MemberDetailsButton_Click;
            #endregion

            #region GROUP BOX
            var memberGroup = new GroupBox
            {
                Height = height,
                MinHeight = 460,
                MinWidth = 260,
                MaxWidth = 260,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = new SolidColorBrush(Color.FromRgb(0x3D, 0x8E, 0x88)),
                Header = new GroupBox
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = Brushes.CadetBlue,
                    Content = new TextBlock
                    {
                        Text = member.Name,
                        MaxHeight = 100,
                        MaxWidth = 220,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 20,
                    },
                },
            };
            memberButton.Content = memberGroup;
            #endregion

            #region STACK PANEL
            var stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.1,
                },
            };
            memberGroup.Content = stackPanel;

            var mainPositionName = new TextBlock
            {
                Text = member.MainPosition.ToString(),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 12,
            };
            stackPanel.Children.Add(mainPositionName);

            var imgGrid = new Grid
            {
                MinHeight = 30,
                MaxHeight = 180,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            stackPanel.Children.Add(imgGrid);

            var memberLeagueImg = new Image
            {
                MinHeight = 30,
                MaxHeight = 180,
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri(ImagesService.GetPositionImageUrl(member.MainPosition), UriKind.Relative)),
            };
            imgGrid.Children.Add(memberLeagueImg);

            if (_cash.User?.Team is not null && Team.Id == _cash.User.Team.Id && member.SkillPoints > 0)
            {
                var levelUp = new TextBlock
                {
                    Background = Brushes.CadetBlue,
                    FontSize = 26,
                    FontFamily = new FontFamily("Arial Black"),
                    Text = "LEVEL UP!",
                    Margin = new Thickness(2),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment= VerticalAlignment.Bottom,
                    Tag = member,
                };

                imgGrid.Children.Add(levelUp);
            }

            var parameters = GetParametersView(member);
            stackPanel.Children.Add(parameters);
            #endregion

            return memberButton;
        }

        private Grid GetParametersView(MemberModel member)
        {
            var resultGrid = new Grid();

            resultGrid.ColumnDefinitions.Add(new ColumnDefinition());
            resultGrid.ColumnDefinitions.Add(new ColumnDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());
            resultGrid.RowDefinitions.Add(new RowDefinition());

            var age = new TextBlock
            {
                Text = "Age:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var ageValue = new TextBlock
            {
                Text = member.Age.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var attack = new TextBlock
            {
                Text = "Attack:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var attackValue = new TextBlock
            {
                Text = member.Attack.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
                Foreground = new SolidColorBrush
                {
                    Color = Brusher.GetColorByLevel(member.Attack),
                },
            };

            var defense = new TextBlock
            {
                Text = "Defense:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var defenseValue = new TextBlock
            {
                Text = member.Defense.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
                Foreground = new SolidColorBrush
                {
                    Color = Brusher.GetColorByLevel(member.Defense),
                },
            };

            var experience = new TextBlock
            {
                Text = "Experience:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var experienceValue = new TextBlock
            {
                Text = member.Experience.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
                Foreground = new SolidColorBrush
                {
                    Color = Brusher.GetColorByLevel(member.Experience),
                },
            };

            var skillPoints = new TextBlock
            {
                Text = "Skill points:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var skillPointsValue = new TextBlock
            {
                Text = member.SkillPoints.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var rankPoints = new TextBlock
            {
                Text = "Rank points:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var rankPointsValue = new TextBlock
            {
                Text = member.RankPoints.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
                Foreground = new SolidColorBrush
                {
                    Color = Brusher.GetColorByLevel(member.RankPoints),
                },
            };

            var energy = new TextBlock
            {
                Text = "Energy:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var energyValue = new ProgressBar
            {
                MaxWidth = 120,
                Background = new SolidColorBrush(Color.FromRgb(0x3D, 0x8E, 0x88)),
                Foreground = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.6,
                },
                Minimum = 0,
                Maximum = member.MaxEnergy,
                Value = member.Energy,
                BorderThickness = new Thickness(2),
            };

            var mentalPower = new TextBlock
            {
                Text = "Mental power:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var mentalPowerValue = new TextBlock
            {
                Text = member.MentalPower.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
                Foreground = new SolidColorBrush
                {
                    Color = Brusher.GetColorByLevel(member.MentalPower),
                },
            };

            var mentalHealth = new TextBlock
            {
                Text = "Mental health:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var mentalHealthValue = new TextBlock
            {
                Text = member.MentalHealth.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var teamplay = new TextBlock
            {
                Text = "Teamplay:",
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Left,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
            };

            var teamplayValue = new TextBlock
            {
                Text = member.Teamplay.ToString(),
                FontFamily = new FontFamily("Arial Black"),
                FontSize = 16,
                TextAlignment = TextAlignment.Right,
                Background = new SolidColorBrush
                {
                    Color = new Color { R = 0, G = 0, B = 0, },
                    Opacity = 0.2,
                },
                Foreground = new SolidColorBrush
                {
                    Color = Brusher.GetColorByLevel(member.Teamplay),
                },
            };

            resultGrid.Children.Add(age);
            resultGrid.Children.Add(ageValue);
            resultGrid.Children.Add(attack);
            resultGrid.Children.Add(attackValue);
            resultGrid.Children.Add(defense);
            resultGrid.Children.Add(defenseValue);
            resultGrid.Children.Add(experience);
            resultGrid.Children.Add(experienceValue);
            resultGrid.Children.Add(skillPoints);
            resultGrid.Children.Add(skillPointsValue);
            resultGrid.Children.Add(rankPoints);
            resultGrid.Children.Add(rankPointsValue);
            resultGrid.Children.Add(energy);
            resultGrid.Children.Add(energyValue);
            resultGrid.Children.Add(mentalPower);
            resultGrid.Children.Add(mentalPowerValue);
            resultGrid.Children.Add(mentalHealth);
            resultGrid.Children.Add(mentalHealthValue);
            resultGrid.Children.Add(teamplay);
            resultGrid.Children.Add(teamplayValue);

            Grid.SetColumn(age, 0);
            Grid.SetColumn(ageValue, 1);
            Grid.SetColumn(attack, 0);
            Grid.SetColumn(attackValue, 1);
            Grid.SetColumn(defense, 0);
            Grid.SetColumn(defenseValue, 1);
            Grid.SetColumn(experience, 0);
            Grid.SetColumn(experienceValue, 1);
            Grid.SetColumn(skillPoints, 0);
            Grid.SetColumn(skillPointsValue, 1);
            Grid.SetColumn(rankPoints, 0);
            Grid.SetColumn(rankPointsValue, 1);
            Grid.SetColumn(energy, 0);
            Grid.SetColumn(energyValue, 1);
            Grid.SetColumn(mentalPower, 0);
            Grid.SetColumn(mentalPowerValue, 1);
            Grid.SetColumn(mentalHealth, 0);
            Grid.SetColumn(mentalHealthValue, 1);
            Grid.SetColumn(teamplay, 0);
            Grid.SetColumn(teamplayValue, 1);

            Grid.SetRow(age, 0);
            Grid.SetRow(ageValue, 0);
            Grid.SetRow(attack, 1);
            Grid.SetRow(attackValue, 1);
            Grid.SetRow(defense, 2);
            Grid.SetRow(defenseValue, 2);
            Grid.SetRow(experience, 3);
            Grid.SetRow(experienceValue, 3);
            Grid.SetRow(skillPoints, 4);
            Grid.SetRow(skillPointsValue, 4);
            Grid.SetRow(rankPoints, 5);
            Grid.SetRow(rankPointsValue, 5);
            Grid.SetRow(energy, 6);
            Grid.SetRow(energyValue, 6);
            Grid.SetRow(mentalPower, 7);
            Grid.SetRow(mentalPowerValue, 7);
            Grid.SetRow(mentalHealth, 8);
            Grid.SetRow(mentalHealthValue, 8);
            Grid.SetRow(teamplay, 9);
            Grid.SetRow(teamplayValue, 9);

            return resultGrid;
        }
    }
}
