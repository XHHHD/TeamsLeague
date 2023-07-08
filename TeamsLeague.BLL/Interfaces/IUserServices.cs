using TeamsLeague.BLL.Models;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IUserServices
    {
        UserModel CreateUser(UserModel user);
        UserModel ReadUser(int userId);
        IEnumerable<UserModel> GetUsers();
        UserModel UpdateUser(UserModel user);
        bool DeleteUser(int userId);
    }
}
