using TeamsLeague.BLL.Models;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IUserServices
    {
        UserModel CreateUser(UserModel user);
        IEnumerable<UserModel> GetAllUsers();
        UserModel ReadUser(int userId);
        UserModel UpdateUser(UserModel user);
        bool DeleteUser(int userId);
    }
}
