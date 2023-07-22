using System.Linq;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.UI.WPF.Views.Windows.TeamEvents;

namespace TeamsLeague.UI.WPF.Views.Pages.Events.SoloRank
{
    public partial class SRMatchPage : Page
    {
        private readonly IMatchBuilder _matchBuilder;
        private readonly IMemberService _memberService;
        private readonly SoloRankWindow _eventWindow;
        private readonly MatchModel _matchModel;


        public SRMatchPage(IMatchBuilder matchBuilder, IMemberService memberService, int memberId, SoloRankWindow eventWindow)
        {
            _matchBuilder = matchBuilder;
            _memberService = memberService;
            _eventWindow = eventWindow;

            _matchModel = _matchBuilder.SetupSoloRank(memberId).GetMatch();

            InitializeComponent();
            ShowPreMatchLobby();
        }


        private void ShowPreMatchLobby()
        {
            _eventWindow.CreateAndShowNextButton(this, true, "START");
            _eventWindow.DeleteBackButton();

            BuildComponent();
        }


        internal void Match()
        {
            var matchId = 999;      //<<<< ADD GAME MECHANIC HERE! <<<<
                                    //<<<< ADD GAME MECHANIC HERE! <<<<
                                    //<<<< ADD GAME MECHANIC HERE! <<<<
                                    //<<<< ADD GAME MECHANIC HERE! <<<<

            _eventWindow.CreateAndShowNextButton(matchId, true);
        }

        private void BuildComponent()
        {
            ViewTeams();
        }

        private void ViewTeams()
        {
            //TEAM A
            var lTop = _matchModel.TeamA.First(s => s.Position == PositionType.Top);
            var lJun = _matchModel.TeamA.First(s => s.Position == PositionType.Jungle);
            var lMid = _matchModel.TeamA.First(s => s.Position == PositionType.Mid);
            var lBot = _matchModel.TeamA.First(s => s.Position == PositionType.Bot);
            var lSup = _matchModel.TeamA.First(s => s.Position == PositionType.Support);

            L_Player1_Name.Text = lTop.Member.Name;
            L_Player2_Name.Text = lJun.Member.Name;
            L_Player3_Name.Text = lMid.Member.Name;
            L_Player4_Name.Text = lBot.Member.Name;
            L_Player5_Name.Text = lSup.Member.Name;

            L_Player1_Team.Text = lTop.Member.Team is not null ? "[" + lTop.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            L_Player2_Team.Text = lJun.Member.Team is not null ? "[" + lJun.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            L_Player3_Team.Text = lMid.Member.Team is not null ? "[" + lMid.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            L_Player4_Team.Text = lBot.Member.Team is not null ? "[" + lBot.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            L_Player5_Team.Text = lSup.Member.Team is not null ? "[" + lSup.Member.Team.Name.Remove(3).ToUpper() + "]" : "";

            L_Player1_Experience.Text = lTop.Member.Experience.ToString();
            L_Player2_Experience.Text = lJun.Member.Experience.ToString();
            L_Player3_Experience.Text = lMid.Member.Experience.ToString();
            L_Player4_Experience.Text = lBot.Member.Experience.ToString();
            L_Player5_Experience.Text = lSup.Member.Experience.ToString();

            L_Player1_RankPoints.Text = lTop.Member.RankPoints.ToString();
            L_Player2_RankPoints.Text = lJun.Member.RankPoints.ToString();
            L_Player3_RankPoints.Text = lMid.Member.RankPoints.ToString();
            L_Player4_RankPoints.Text = lBot.Member.RankPoints.ToString();
            L_Player5_RankPoints.Text = lSup.Member.RankPoints.ToString();


            //TEAM B
            var rTop = _matchModel.TeamB.First(s => s.Position == PositionType.Top);
            var rJun = _matchModel.TeamB.First(s => s.Position == PositionType.Jungle);
            var rMid = _matchModel.TeamB.First(s => s.Position == PositionType.Mid);
            var rBot = _matchModel.TeamB.First(s => s.Position == PositionType.Bot);
            var rSup = _matchModel.TeamB.First(s => s.Position == PositionType.Support);

            R_Player1_Name.Text = rTop.Member.Name;
            R_Player2_Name.Text = rJun.Member.Name;
            R_Player3_Name.Text = rMid.Member.Name;
            R_Player4_Name.Text = rBot.Member.Name;
            R_Player5_Name.Text = rSup.Member.Name;

            R_Player1_Team.Text = rTop.Member.Team is not null ? "[" + rTop.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            R_Player2_Team.Text = rJun.Member.Team is not null ? "[" + rJun.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            R_Player3_Team.Text = rMid.Member.Team is not null ? "[" + rMid.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            R_Player4_Team.Text = rBot.Member.Team is not null ? "[" + rBot.Member.Team.Name.Remove(3).ToUpper() + "]" : "";
            R_Player5_Team.Text = rSup.Member.Team is not null ? "[" + rSup.Member.Team.Name.Remove(3).ToUpper() + "]" : "";

            R_Player1_Experience.Text = rTop.Member.Experience.ToString();
            R_Player2_Experience.Text = rJun.Member.Experience.ToString();
            R_Player3_Experience.Text = rMid.Member.Experience.ToString();
            R_Player4_Experience.Text = rBot.Member.Experience.ToString();
            R_Player5_Experience.Text = rSup.Member.Experience.ToString();

            R_Player1_RankPoints.Text = rTop.Member.RankPoints.ToString();
            R_Player2_RankPoints.Text = rJun.Member.RankPoints.ToString();
            R_Player3_RankPoints.Text = rMid.Member.RankPoints.ToString();
            R_Player4_RankPoints.Text = rBot.Member.RankPoints.ToString();
            R_Player5_RankPoints.Text = rSup.Member.RankPoints.ToString();
        }
    }
}
