using TeamsLeague.BLL.Models;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.UI.WPF.Windows;

namespace TeamsLeague.UI.WPF.Buffer
{
    public class CashBasket : ICashBasket
    {
        public UserModel? User { get; set; }
        public GameWindow? GameWindow { get; set; }
    }
}