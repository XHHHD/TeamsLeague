using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class UserStatsMenu : Page
    {
        private readonly IBufferingService _buffer;

        public UserStatsMenu(IBufferingService buffer)
        {
            _buffer = buffer;
            InitializeComponent();
            BuildComponent();
        }

        private void BuildComponent()
        {
            UserName_TextBlock.Text = _buffer.User is not null ? _buffer.User.Name : "Adm";
            MatchHistory_StackPanel.Children.Add(new ListBox() { Margin = new Thickness(3) , MinHeight = 50 });
        }
    }
}
