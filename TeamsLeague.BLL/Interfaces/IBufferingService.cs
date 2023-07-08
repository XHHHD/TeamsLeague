using TeamsLeague.BLL.Models;
using TeamsLeague.DAL.Entities;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IBufferingService
    {
        UserModel? User { get; set; }
    }
}
