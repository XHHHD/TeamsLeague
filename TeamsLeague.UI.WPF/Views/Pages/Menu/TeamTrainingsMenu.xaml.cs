using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class TeamTrainingsMenu : Page
    {
        private readonly IBufferingService _buffer;

        public TeamTrainingsMenu(IBufferingService buffer)
        {
            _buffer = buffer;
            InitializeComponent();
            BuildComponent();
        }

        private void BuildComponent()
        {
            TeamName_TextBlock.Text = _buffer.User?.Team is not null ? _buffer.User?.Team.Name : "Team Name";
            TeamShortName_TextBlock.Text = _buffer.User?.Team is not null ? _buffer.User?.Team.Name.ToUpper().Remove(3) : "ONE";
        }
    }
}
