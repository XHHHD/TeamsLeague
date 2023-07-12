using TeamsLeague.BLL.Models;
using TeamsLeague.UI.WPF.Windows;

namespace TeamsLeague.UI.WPF.Buffer
{
    public interface ICashBasket
    {
        UserModel? User { get; set; }
        GameWindow? GameWindow { get; set; }
    }
}
