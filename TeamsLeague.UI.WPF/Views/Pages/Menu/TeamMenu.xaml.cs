using System.Windows.Controls;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class TeamMenu : Page
    {
        private readonly TeamModel _team;


        public TeamMenu(TeamModel team)
        {
            _team = team;
            InitializeComponent();
            BuildComponent();
        }


        private void BuildComponent()
        {
            TeamName_TextBlock.Text = _team.Name;

            foreach (var member in _team.Members)
            {
                Members_StackPanel.Children.Add(GetMemberViews(member));
            }
        }

        private Grid GetMemberViews(MemberModel member)
        {
            var mainMemberGrid = new Grid();
            return mainMemberGrid;
        }
    }
}
