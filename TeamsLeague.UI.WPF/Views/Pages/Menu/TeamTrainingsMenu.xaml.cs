using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.UI.WPF.Buffer;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Views.Windows;
using TeamsLeague.UI.WPF.Views.Windows.Member;
using TeamsLeague.UI.WPF.Views.Windows.TeamEvents;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class TeamTrainingsMenu : Page
    {
        private const string noAssignedMember = "There is no assigned member.";

        private readonly ICashBasket _cash;
        private readonly ITeamService _teamService;
        private readonly IMemberService _memberService;

        private TeamModel Team { get; set; }
        private IEnumerable<MemberModel> Members { get; set; }

        public TeamTrainingsMenu(ICashBasket cash, ITeamService teamService, IMemberService memberService)
        {
            _cash = cash;
            _teamService = teamService;
            _memberService = memberService;

            if (_cash.User?.Team is not null)
            {
                Team = _teamService.GetTeam(_cash.User.Team.Id);
                Members = _memberService.GetMembersOfTeam(_cash.User.Team.Id);
            }

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

        private void Act1_SoloRank_Click(object sender, RoutedEventArgs e)
        {
            var soloRankEvent = UnityContainerProvider.GetNew<SoloRankWindow>(new ParameterOverride("teamId", Team.Id));

            IsEnabled = false;
            soloRankEvent.Closed += (s, args) =>
            {
                IsEnabled = true;
                BuildComponent();
            };

            soloRankEvent.ShowDialog();
        }

        private void Act2_GroupTrain_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Act3_Screams_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Act4_GroupRank_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Act5_Tournament_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BuildComponent()
        {
            Team = _teamService.GetTeam(Team.Id);
            Members = _memberService.GetMembersOfTeam(Team.Id);

            TeamName_TextBlock.Text = _cash.User?.Team is not null ? _cash.User?.Team.Name : "Team Name";
            TeamShortName_TextBlock.Text = _cash.User?.Team is not null ? _cash.User?.Team.Name.ToUpper().Remove(3) : "ONE";
            TeamLogo.Source = new BitmapImage(new Uri(Team.Image ?? "/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative));

            FillInTeamStats();

            FillInMembersButtons();
        }

        private void FillInTeamStats()
        {
            Honor_TextBlock.Text = Team.Honor.ToString();

            Experience_TextBlock.Text = Team.Experience.ToString();

            RankPoints_TextBlock.Text = Team.RankPoints.ToString();

            Teamplay_TextBlock.Text= Team.Teamplay.ToString();

            Energy_Progress.Value = Team.Energy;
            Energy_Progress.Maximum = Team.MaxEnergy;

            Health_Progress.Value = Team.Health;
            Health_Progress.Maximum = Team.MaxHealth;
        }

        private void FillInMembersButtons()
        {
            var top = Members.FirstOrDefault(m => m.MainPosition == PositionType.Top);
            var jun = Members.FirstOrDefault(m => m.MainPosition == PositionType.Jungle);
            var mid = Members.FirstOrDefault(m => m.MainPosition == PositionType.Mid);
            var bot = Members.FirstOrDefault(m => m.MainPosition == PositionType.Bot);
            var sup = Members.FirstOrDefault(m => m.MainPosition == PositionType.Support);

            TopLiner_Button.Content = top?.Name ?? noAssignedMember;
            TopLiner_Button.Tag = top;
            TopLiner_Button.Click += top is null ? AddMemberButton_Click : MemberDetailsButton_Click;

            Jungler_Button.Content = jun?.Name ?? noAssignedMember;
            Jungler_Button.Tag = jun;
            Jungler_Button.Click += jun is null ? AddMemberButton_Click : MemberDetailsButton_Click;

            Midliner_Button.Content = mid?.Name ?? noAssignedMember;
            Midliner_Button.Tag = mid;
            Midliner_Button.Click += mid is null ? AddMemberButton_Click : MemberDetailsButton_Click;

            Bottliner_Button.Content = bot?.Name ?? noAssignedMember;
            Bottliner_Button.Tag = bot;
            Bottliner_Button.Click += bot is null ? AddMemberButton_Click : MemberDetailsButton_Click;

            Support_Button.Content = sup?.Name ?? noAssignedMember;
            Support_Button.Tag = sup;
            Support_Button.Click += sup is null ? AddMemberButton_Click : MemberDetailsButton_Click;
        }
    }
}
