using Microsoft.EntityFrameworkCore;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Services
{
    public class TeamService : ITeamService
    {
        private readonly GameDBContext _context;


        public TeamService(GameDBContext context)
        {
            _context = context;
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


            var team = new Team
            {
                Name = teamModel.Name,
                Image = teamModel.Image,

                Experience = teamModel.Experience,
                RankPoints = teamModel.RankPoints,
                Honor = teamModel.Honor,

                Energy = teamModel.Energy,
                MaxEnergy = teamModel.MaxEnergy,
                Health = teamModel.Health,
                MaxHealth = teamModel.MaxHealth,
                Teamplay = teamModel.Teamplay,

                User = user,
            };


            _context.Teams.Add(team);
            _context.SaveChanges();


            teamModel.Id = team.Id;

            return teamModel;
        }

        public IEnumerable<TeamModel> GetTeams()
        {
            var teams = _context.Teams
                .Include(t => t.Members)
                .Include(t => t.Traits);

            var result = teams.Select(t => new TeamModel
            {
                Name = t.Name,
                Image = t.Image,

                Experience = t.Experience,
                RankPoints = t.RankPoints,
                Honor = t.Honor,

                Energy = t.Energy,
                MaxEnergy = t.MaxEnergy,
                Health = t.Health,
                MaxHealth = t.MaxHealth,
                Teamplay = t.Teamplay,

                User = t.User != null
                ? new UserModel
                {
                    Id = t.User.Id,
                    Name = t.User.Name,
                } : null,

                Members = t.Members.Select(m => new MemberModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age,
                    Attack = m.Attack,
                    Defense = m.Defense,
                    CreationDate = m.CreationDate,
                    LastChanges = m.LastChanges,

                    Experience = m.Experience,
                    SkillPoints = m.SkillPoints,
                    RankPoints = m.RankPoints,

                    Energy = m.Energy,
                    MaxEnergy = m.MaxEnergy,
                    MentalPower = m.MentalPower,
                    MaxMentalPower = m.MaxMentalPower,
                    MentalHealth = m.MentalHealth,
                    MaxMentalHealth = m.MaxMentalHealth,
                    Teamplay = m.Teamplay,
                    MinTeamplay = m.MinTeamplay,
                    MaxTeamplay = m.MaxTeamplay,

                    Team = m.Team != null
                    ? new TeamModel
                    {
                        Id = m.Team.Id,
                        Name = m.Team.Name,
                    }
                    : null,

                    Positions = m.Positions.Select(p => new PositionModel
                    {
                        Id = p.Id,
                        Type = p.Type,
                    }).ToHashSet(),

                    Traits = m.Traits.Select(t => new MemberTraitModel
                    {
                        Id = t.Id,
                        Type = t.Type
                    }).ToHashSet()
                }).ToHashSet(),

                Traits = t.Traits.Select(tr => new TeamTraitModel
                {
                    Id = tr.Id,
                    Type = tr.Type,
                }).ToHashSet(),
            });

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

            var result = new TeamModel
            {
                Name = team.Name,
                Image = team.Image,

                Experience = team.Experience,
                RankPoints = team.RankPoints,
                Honor = team.Honor,

                Energy = team.Energy,
                MaxEnergy = team.MaxEnergy,
                Health = team.Health,
                MaxHealth = team.MaxHealth,
                Teamplay = team.Teamplay,

                User = team.User != null
                ? new UserModel
                {
                    Id = team.User.Id,
                    Name = team.User.Name,
                } : null,

                Members = team.Members.Select(m => new MemberModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Age = m.Age,
                    Attack = m.Attack,
                    Defense = m.Defense,
                    CreationDate = m.CreationDate,
                    LastChanges = m.LastChanges,

                    Experience = m.Experience,
                    SkillPoints = m.SkillPoints,
                    RankPoints = m.RankPoints,

                    Energy = m.Energy,
                    MaxEnergy = m.MaxEnergy,
                    MentalPower = m.MentalPower,
                    MaxMentalPower = m.MaxMentalPower,
                    MentalHealth = m.MentalHealth,
                    MaxMentalHealth = m.MaxMentalHealth,
                    Teamplay = m.Teamplay,
                    MinTeamplay = m.MinTeamplay,
                    MaxTeamplay = m.MaxTeamplay,

                    Team = m.Team != null
                    ? new TeamModel
                    {
                        Id = m.Team.Id,
                        Name = m.Team.Name,
                    }
                    : null,

                    Positions = m.Positions.Select(p => new PositionModel
                    {
                        Id = p.Id,
                        Type = p.Type,
                    }).ToHashSet(),

                    Traits = m.Traits.Select(t => new MemberTraitModel
                    {
                        Id = t.Id,
                        Type = t.Type
                    }).ToHashSet()
                }).ToHashSet(),

                Traits = team.Traits.Select(tr => new TeamTraitModel
                {
                    Id = tr.Id,
                    Type = tr.Type,
                }).ToHashSet(),
            };

            return result;
        }

        public TeamModel UpdateTeam(TeamModel teamModel)
        {
            var team = _context.Teams
                .Include(t => t.User)
                .Include(t => t.Members)
                .Include(t => t.Traits)
                .FirstOrDefault(t => t.Id == teamModel.Id)
                ?? throw new Exception("Team does not exist!");

            team.Name = teamModel.Name;
            team.Image = teamModel.Image;

            team.Experience = teamModel.Experience;
            team.RankPoints = teamModel.RankPoints;
            team.Honor = teamModel.Honor;

            team.Energy = teamModel.Energy;
            team.MaxEnergy = teamModel.MaxEnergy;
            team.Health = teamModel.Health;
            team.MaxHealth = teamModel.MaxHealth;
            team.Teamplay = teamModel.Teamplay;

            team.User = teamModel.User != null
            ? new User
            {
                Id = teamModel.User.Id,
                Name = teamModel.User.Name,
            } : null;

            team.Members = teamModel.Members.Select(m => new Member
            {
                Id = m.Id,
                Name = m.Name,
                Age = m.Age,
                Attack = m.Attack,
                Defense = m.Defense,
                CreationDate = m.CreationDate,
                LastChanges = m.LastChanges,

                Experience = m.Experience,
                SkillPoints = m.SkillPoints,
                RankPoints = m.RankPoints,

                Energy = m.Energy,
                MaxEnergy = m.MaxEnergy,
                MentalPower = m.MentalPower,
                MaxMentalPower = m.MaxMentalPower,
                MentalHealth = m.MentalHealth,
                MaxMentalHealth = m.MaxMentalHealth,
                Teamplay = m.Teamplay,
                MinTeamplay = m.MinTeamplay,
                MaxTeamplay = m.MaxTeamplay,

                Team = m.Team != null
                ? new Team
                {
                    Id = m.Team.Id,
                    Name = m.Team.Name,
                } : null,

                Positions = m.Positions.Select(p => new Position
                {
                    Id = p.Id,
                    Type = p.Type,
                }).ToHashSet(),

                Traits = m.Traits.Select(t => new MemberTrait
                {
                    Id = t.Id,
                    Type = t.Type
                }).ToHashSet()
            }).ToHashSet();

            team.Traits = teamModel.Traits.Select(tr => new TeamTrait
            {
                Id = tr.Id,
                Type = tr.Type,
            }).ToHashSet();

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

        public bool IsTeamNameIsFree(string teamName) => _context.Teams.FirstOrDefault(t => t.Name == teamName) is null;
    }
}
