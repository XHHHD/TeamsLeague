using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.UI.WPF.Views.Windows.TeamEvents;

namespace TeamsLeague.UI.WPF.Views.Pages.Events.SoloRank
{
    public partial class SRMatchPage : Page
    {
        private readonly IMatchService _matchService;
        private readonly IMemberService _memberService;
        private readonly SoloRankWindow _eventWindow;

        private MemberModel Member { get; set; }
        private List<MemberModel> Members { get; set; }


        public SRMatchPage(IMatchService matchService, IMemberService memberService, int memberId, SoloRankWindow eventWindow)
        {
            _matchService = matchService;
            _memberService = memberService;
            _eventWindow = eventWindow;

            Member = _memberService.GetMember(memberId);

            InitializeComponent();
            ShowPreMatchLobby();
        }


        private void ShowPreMatchLobby()
        {
            _eventWindow.CreateAndShowNextButton(this, true, "START");
            _eventWindow.DeleteBackButton();
        }


        internal void Match()
        {
            var matchId = 999;      //<<<< ADD GAME MECHANIC HERE! <<<<
                                    //<<<< ADD GAME MECHANIC HERE! <<<<
                                    //<<<< ADD GAME MECHANIC HERE! <<<<
                                    //<<<< ADD GAME MECHANIC HERE! <<<<

            _eventWindow.CreateAndShowNextButton(matchId, true);
        }

        public void BuildComponent()
        {
            var memberView = BuildMemberView(Member);

            L_Player1_StackPanel.Children.Add(memberView);
        }

        public TextBlock BuildMemberView(MemberModel member)
        {
            var memberName = new TextBlock
            {
                Text = member.Name,
                FontSize = 32,
            };

            return memberName;
        }
    }
}
