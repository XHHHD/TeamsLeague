using Microsoft.EntityFrameworkCore;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Services
{
    public class TeamServices : ITeamServices
    {
        GameDBContext _context;

        public TeamServices()
        {
            _context = new();
        }

        public TeamModel CreateTeam(TeamModel teamModel)
        {
            if (_context.Teams.FirstOrDefault(t => t.Name == teamModel.Name) != null)
            { throw new Exception("Team with this name is already exist!"); }

            User? user = null;
            if (teamModel.User is not null)
            {
                user = _context.Users.FirstOrDefault(u => u.Id == teamModel.User.Id)
                    ?? throw new Exception("User wasn't found!");
            }


            var team = new Team()
            {
                Name = teamModel.Name,
                User = user,
            };


            _context.Teams.Add(team);
            _context.SaveChanges();

            return teamModel;
        }

        public bool DeleteTeam(int teamId)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == teamId)
                ?? throw new Exception("Team does not exist!");

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<TeamModel> GetTeams()
        {
            var teams = _context.Teams;

            var result = teams.Select(t => new TeamModel(t));

            return result;
        }

        public TeamModel ReadTeam(int teamId)
        {
            var team = _context.Teams
                .Include(t => t.Members)
                .ThenInclude(m => m.Positions)
                .Include(t => t.Members)
                .ThenInclude(m => m.Traits)
                .Include(t => t.Traits)
                .SingleOrDefault(t => t.Id == teamId)
                ?? throw new Exception("Team does not exist!");

            var result = new TeamModel(team);

            return result;
        }

        public TeamModel UpdateTeam(TeamModel teamModel)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == teamModel.Id)
                ?? throw new Exception("Team does not exist!");

            team.Name = teamModel.Name;

            _context.SaveChanges();

            return teamModel;
        }
    }
}
