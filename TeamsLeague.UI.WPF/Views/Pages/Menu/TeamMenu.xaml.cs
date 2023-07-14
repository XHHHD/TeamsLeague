using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.BLL.Services;
using TeamsLeague.DAL.Constants;
using TeamsLeague.UI.WPF.Buffer;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Views.Windows;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class TeamMenu : Page
    {
        private readonly object _creator;
        private readonly ICashBasket _cash;
        private readonly ITeamService _teamService;
        private readonly TeamModel _team;


        public TeamMenu(ICashBasket cash, ITeamService teamService, int teamId, object creator)
        {
            _creator = creator;
            _cash = cash;
            _teamService = teamService;
            _team = _teamService.GetTeam(teamId);

            InitializeComponent();
            BuildComponent();
        }


        private void MemberDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                if (button.Tag is MemberModel memberModel)
                {
                    var memberWindow = UnityContainerProvider.GetNew<MemberDetailsWindow>(new ParameterOverride("memberId", memberModel.Id));

                    memberWindow.ShowDialog();
                    BuildComponent();
                }
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _cash.GameWindow.GameMainFrame.Content = _creator;
        }

        private void BuildComponent()
        {
            TeamName_TextBlock.Text = _team.Name;

            FillInStats();

            Members_StackPanel.Children.Clear();
            foreach (var member in _team.Members)
            {
                Members_StackPanel.Children.Add(GetMemberViews(member));
                Members_StackPanel.Background = null;
            }

            if (_team.Id == _cash.User?.Team.Id && _cash.User?.Team.Members.Count < Enum.GetValues(typeof(PositionType)).Length)
            {
                Members_StackPanel.Children.Add(new Button
                {
                    Width = 240,
                    Margin = new Thickness(0),
                    Background = null,
                    Foreground = new SolidColorBrush(Color.FromArgb(120, 189, 189, 189)),
                    FontFamily = new FontFamily("Arial Black"),
                    FontSize = 120,
                    Content = "+",
                });
            }
        }

        private void FillInStats()
        {
            Health_Progress.Value = _team.Health;
            Health_Progress.Maximum = _team.MaxHealth;
            Energy_Progress.Value = _team.Energy;
            Energy_Progress.Maximum = _team.MaxEnergy;
            Honor_TextBlock.Text = _team.Honor.ToString();
            Experience_TextBlock.Text = _team.Experience.ToString();
            RankPoints_TextBlock.Text = _team.RankPoints.ToString();
            Teamplay_TextBlock.Text = _team.Teamplay.ToString();    //IN FEATURE NUMBER WILL BE HIDDEN AND SHOW ONLY MENTHAL LEVEL
        }

        private Button GetMemberViews(MemberModel member)
        {
            var mainMemberButton = new Button
            {
                MinWidth = 240,
                MaxWidth = 300,
                Background = null,
                BorderThickness = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Stretch,
                Tag = member,
            };
            mainMemberButton.Click += MemberDetailsButton_Click;

            var memberGroup = new GroupBox
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = new SolidColorBrush(Color.FromRgb(0x3D, 0x8E, 0x88)),
                Header = new GroupBox
                {
                    Background = Brushes.CadetBlue,
                    Content = new TextBlock
                    {
                        Text = member.Name,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 20,
                    },
                },
            };
            mainMemberButton.Content = memberGroup;

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

            var memberLeagueImg = new Image
            {
                MinHeight = 30,
                MaxHeight = 180,
                Margin = new Thickness(20),
                Source = new BitmapImage(new Uri(ImagesService.GetPositionImageUrl(member.MainPosition), UriKind.Relative)),
            };
            stackPanel.Children.Add(memberLeagueImg);

            var parameters = GetParametersView(member);

            stackPanel.Children.Add(parameters);

            return mainMemberButton;
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
