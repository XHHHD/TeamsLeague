using System.Windows;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;

namespace TeamsLeague.UI.WPF.Views.Windows.TeamEvents
{
    public partial class SoloRankWindow : Window
    {
        private readonly IGameMech _gameMech;
        private readonly IMemberService _memberService;

        private MemberModel Member { get; set; }


        public SoloRankWindow(IGameMech gameMech, IMemberService memberService, int memberId)
        {
            _gameMech = gameMech;
            _memberService = memberService;
            Member = _memberService.GetMember(memberId);

            InitializeComponent();
        }


        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
