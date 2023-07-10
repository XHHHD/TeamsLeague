using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class LeagueMenu : Page
    {
        private readonly IBufferingService _buffer;


        public LeagueMenu(IBufferingService buffer)
        {
            _buffer = buffer;

            InitializeComponent();
            BuildComponent();
        }


        private void BuildComponent()
        {
            TeamsLeague_StackPanel.Children.Add(new TextBox() { Text = "NO DATA FOUND!", FontSize = 40 });
        }
    }
}
