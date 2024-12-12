using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Services;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Views.Windows.Member;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Views.Windows
{
    public partial class MemberDetailsWindow : Window
    {
        private readonly IMemberService _memberService;

        private MemberModel Member { get; set; }


        public MemberDetailsWindow(IMemberService memberService, int memberId)
        {
            _memberService = memberService;
            Member = _memberService.GetMember(memberId);

            InitializeComponent();
            BuildComponent();
        }


        private void GameWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LvlUp_Button_Click(object sender, RoutedEventArgs e)
        {
            var levelUp = UnityContainerProvider.GetNew<MemberLevelUpWindow>(new ParameterOverride("memberModel", Member));

            this.IsEnabled = false;

            levelUp.Closed += (s, args) =>
            {
                IsEnabled = true;
                BuildComponent();
            };

            levelUp.ShowDialog();
        }

        private void BuildComponent()
        {
            Member = _memberService.GetMember(Member.Id);

            LvlUp_Button.IsEnabled = Member.SkillPoints > 0;
            FreeSkillPoints.IsEnabled = Member.SkillPoints > 0;
            LvlUp_Button.Visibility = Member.SkillPoints > 0 ? Visibility.Visible : Visibility.Hidden;
            FreeSkillPoints.Visibility = Member.SkillPoints > 0 ? Visibility.Visible : Visibility.Hidden;

            MemberNameValue.Text = Member.Name;

            RankPointsValue.Text = Member.RankPoints.ToString();

            ExperienceValue.Text = Member.Experience.ToString();

            MemberRoleImg.Source = new BitmapImage(new Uri(ImagesService.GetPositionImageUrl(Member.MainPosition), UriKind.Relative));

            MainPositionValue.Text = Member.MainPosition.ToString();

            AgeValue.Text = Member.Age.ToString();

            AttackValue.Text = Member.Attack.ToString();

            DefenseValue.Text = Member.Defense.ToString();

            MentalPowerValue.Text = Member.MentalPower.ToString();

            MentalHealthValue.Value = Member.MentalHealth;
            MentalHealthValue.Maximum = Member.MaxMentalHealth;

            EnergyValue.Value = Member.Energy;
            EnergyValue.Maximum = Member.MaxEnergy;

            TeamplayValue.Text = Member.Teamplay.ToString();

            FillInMatchHistory();
        }

        private void FillInMatchHistory()
        {
            foreach (var seat in Member.SeatsHistory)
            {
                Match_History.Children.Add(new Button
                {
                    MinHeight = 80,
                    Margin = new Thickness(10),
                    Background = Brushes.CadetBlue,
                    Content = new TextBlock
                    {
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 28,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center,
                        Text = GetResult(seat),
                    },
                });
            }
        }

        private string GetResult(MatchSeatModel seat)
        {
            var result = "";

            if (seat.Match.IsItEnded)
            {
                result += seat.Match.Winer == seat.Side ? "WIN" : "LOSE";

                result += $": {seat.Kills.Count} / {seat.Dead.Count} / {seat.Assists.Count}";
            }
            else
            {
                result += "Match denied!";
            }

            return result;
        }
    }
}
