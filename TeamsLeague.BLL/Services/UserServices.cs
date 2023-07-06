using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities;

namespace TeamsLeague.BLL.Services
{
    public class UserServices : IUserServices
    {
        GameDBContext _context;

        public UserServices(GameDBContext context)
        {
            _context = context;
        }


        public UserModel CreateUser(UserModel userModel)
        {
            if (_context.Users.FirstOrDefault(u => u.Name == userModel.Name) != null)
            { throw new Exception("User is already exist!"); }

            var user = new User()
            {
                Name = userModel.Name,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            userModel.Id = user.Id;

            return userModel;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User is already exist!");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public UserModel ReadUser(int userId)
        {
            throw new NotImplementedException();
        }

        public UserModel UpdateUser(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
