using System;
using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.UI.WPF.Views.Windows.TeamEvents;

namespace TeamsLeague.UI.WPF.Views.Pages.Events.SoloRank
{
    public partial class SRMatchPage : Page
    {
        private readonly IGameMech _gameMech;
        private readonly IMemberService _memberService;
        private readonly SoloRankWindow _eventWindow;

        private MemberModel Member { get; set; }


        public SRMatchPage(IGameMech gameMech, IMemberService memberService, int memberId, SoloRankWindow eventWindow)
        {
            _gameMech = gameMech;
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
    }
}
