using System.Windows;
using TeamsLeague.BLL.Interfaces;

namespace TeamsLeague.UI.WPF.Windows
{
    public partial class LobbyWindow : Window
    {
        private IBufferingService _buffer;

        public LobbyWindow(IBufferingService buffer)
        {
            InitializeComponent();
            _buffer = buffer;
        }
    }
}
