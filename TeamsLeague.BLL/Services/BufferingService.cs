using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.DAL.Entities;

namespace TeamsLeague.BLL.Services
{
    public class BufferingService : IBufferingService
    {
        public UserModel? User { get; set; }
    }
}