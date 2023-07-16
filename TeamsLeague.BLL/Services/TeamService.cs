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
        private readonly IMemberService _memberService;
        private GameDBContext Context { get; set; }


        public TeamService(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public TeamModel CreateTeam(TeamModel teamModel)
        {
            //DB REQUESTS
            Context = new();
            if (Context.Teams.FirstOrDefault(t => t.Name == teamModel.Name) != null)
            { throw new Exception("Team with this name is already exist!"); }

            User? user = null;
            if (teamModel.User is not null)
            {
                user = Context.Users.FirstOrDefault(u => u.Id == teamModel.User.Id)
                    ?? throw new Exception("User wasn't found!");
            }

            //GENERATE TEAM
            var team = new Team
            {
                Name = teamModel.Name,
                Image = teamModel.Image,
                LastChanges = teamModel.LastChanges,

                Experience = teamModel.Experience,
                RankPoints = teamModel.RankPoints,
                Honor = teamModel.Honor,

                Energy = teamModel.Energy,
                MaxEnergy = teamModel.MaxEnergy,
                EnergyRegen = teamModel.EnergyRegen,
                Health = teamModel.Health,
                MaxHealth = teamModel.MaxHealth,
                HealthRegen = teamModel.HealthRegen,
                Teamplay = teamModel.Teamplay,

                User = user,
            };

            //CREATE MEMBERS
            if (teamModel.Members is not null)
            {
                foreach(var memberModel in teamModel.Members)
                {
                    var member = Context.Members.FirstOrDefault(m => m.Name == memberModel.Name);

                    if (member is null)
                    {
                        memberModel.Id = _memberService.CreateMember(memberModel).Id;

                        member = Context.Members.FirstOrDefault(m => m.Id == memberModel.Id);
                    }

                    team.Members.Add(member);
                }
            }


            UpdateStatesToCurrentTime(team);
            Context.Teams.Add(team);
            Context.SaveChanges();
            Context.Dispose();

            teamModel.Id = team.Id;


            return teamModel;
        }

        public IEnumerable<TeamModel> GetAllTeams()
        {
            Context = new();
            var teams = Context.Teams
                .Include(t => t.Members)
                .Include(t => t.Traits);
            foreach (var team in teams) { UpdateStatesToCurrentTime(team); }
            Context.SaveChanges();

            var result = teams.Select(t => new TeamModel(t)).ToList();

            Context.Dispose();

            return result;
        }

        public TeamModel GetTeam(int teamId)
        {
            Context = new();
            var team = Context.Teams
                .Include(t => t.Members)
                .ThenInclude(m => m.Positions)
                .Include(t => t.Members)
                .ThenInclude(m => m.Traits)
                .Include(t => t.Traits)
                .SingleOrDefault(t => t.Id == teamId)
                ?? throw new Exception("Team does not exist!");
            UpdateStatesToCurrentTime(team);
            Context.SaveChanges();

            var result = new TeamModel(team);

            Context.Dispose();

            return result;
        }

        public TeamModel UpdateTeam(TeamModel teamModel)
        {
            Context = new();
            var team = Context.Teams
                .Include(t => t.User)
                .Include(t => t.Members)
                .Include(t => t.Traits)
                .FirstOrDefault(t => t.Id == teamModel.Id)
                ?? throw new Exception("Team does not exist!");
            UpdateStatesToCurrentTime(team);


            team.Name = teamModel.Name;
            team.Image = teamModel.Image;
            team.LastChanges = DateTime.Now;

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
                Intelligence = m.Intelligence,
                ReactionSpeed = m.ReactionSpeed,
                MentalPower = m.MentalPower,
                MentalResistance = m.MentalResistance,
                CreationDate = m.CreationDate,
                LastChanges = m.LastChanges,
                MainPosition = m.MainPosition,

                Experience = m.Experience,
                SkillPoints = m.SkillPoints,
                RankPoints = m.RankPoints,

                Energy = m.Energy,
                MaxEnergy = m.MaxEnergy,
                EnergyRegen = m.EnergyRegen,
                MentalHealth = m.MentalHealth,
                MaxMentalHealth = m.MaxMentalHealth,
                MentalHealthRegen = m.MentalHealthRegen,
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

            Context.SaveChanges();
            Context.Dispose();

            return teamModel;
        }

        public bool DeleteTeam(int teamId)
        {
            Context = new();
            var team = Context.Teams.FirstOrDefault(t => t.Id == teamId)
                ?? throw new Exception("Team does not exist!");

            Context.Teams.Remove(team);
            Context.SaveChanges();
            Context.Dispose();

            return true;
        }

        public bool AddMemberToTheTeam(int teamId, int memberId)
        {
            Context = new();
            var team = Context.Teams.FirstOrDefault(t => t.Id == teamId)
                ?? throw new Exception("Team does not exist!");
            var member = Context.Members.FirstOrDefault(m => m.Id == memberId)
                ?? throw new Exception("Member does not exist!");

            UpdateStatesToCurrentTime(team);
            team.Members.Add(member);

            Context.SaveChanges();
            Context.Dispose();

            return true;
        }

        public bool IsTeamNameIsFree(string teamName) => Context.Teams.AsNoTracking().FirstOrDefault(t => t.Name == teamName) is null;

        private static void UpdateStatesToCurrentTime(Team team)
        {
            var currentTime = DateTime.Now;

            team.Energy += (team.LastChanges - currentTime).TotalSeconds * team.EnergyRegen;
            team.Health += (team.LastChanges - currentTime).TotalSeconds * team.HealthRegen;

            team.Energy += team.Energy > team.MaxEnergy ? team.MaxEnergy : team.Energy;
            team.Health += team.Health > team.MaxHealth ? team.MaxHealth : team.Health;

            team.LastChanges = currentTime;
        }
    }
}
