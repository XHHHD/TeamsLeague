using Microsoft.EntityFrameworkCore;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Services
{
    public class UserServices : IUserServices
    {
        private readonly GameDBContext _context;

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
                ?? throw new Exception("User does not exist!");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public UserModel ReadUser(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId)
                ?? throw new Exception("User does not exist!");

            var result = new UserModel(user);

            return result;
        }

        public UserModel UpdateUser(UserModel userModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userModel.Id)
                ?? throw new Exception("User does not exist!");

            Team? team = null;
            if (userModel.Team is not null)
            {
                team = _context.Teams.SingleOrDefault(t => t.Id == userModel.Team.Id)
                    ?? throw new Exception("Team wasn't found!");
            }

            user.Name = userModel.Name;
            user.Team = team;

            _context.SaveChanges();

            return userModel;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var users = _context.Users.Include(u => u.Team);

            var result = users.Select(u => new UserModel(u));

            return result;
        }
    }
}
