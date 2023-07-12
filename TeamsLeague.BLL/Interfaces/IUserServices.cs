using TeamsLeague.BLL.Models;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IUserServices
    {
        UserModel CreateUser(UserModel user);
        IEnumerable<UserModel> GetUsers();
        UserModel ReadUser(int userId);
        UserModel UpdateUser(UserModel user);
        bool DeleteUser(int userId);
    }
}
