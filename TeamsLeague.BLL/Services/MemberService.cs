using Microsoft.EntityFrameworkCore;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Services
{
    public class MemberService : IMemberService
    {
        private GameDBContext Context { get; set; }


        public MemberService()
        {
        }


        public MemberModel CreateMember(MemberModel memberModel)
        {
            Context = new();
            if (Context.Members.SingleOrDefault(m => m.Name == memberModel.Name) is not null)
            { throw new Exception($"Member with name - {memberModel.Name}, is already exist!"); }

            Team? team = null;
            if (memberModel.Team is not null)
            {
                team = Context.Teams.SingleOrDefault(t => t.Id == memberModel.Team.Id)
                    ?? throw new Exception("Team wasn't found!");
            }


            memberModel.Positions ??= new();
            memberModel.Traits ??= new();

            var member = new Member
            {
                Name = memberModel.Name,
                Age = memberModel.Age,
                Attack = memberModel.Attack,
                Defense = memberModel.Defense,
                Intelligence = memberModel.Intelligence,
                ReactionSpeed = memberModel.ReactionSpeed,
                MentalPower = memberModel.MentalPower,
                MentalResistance = memberModel.MentalResistance,
                CreationDate = memberModel.CreationDate,
                LastChanges = memberModel.LastChanges,
                MainPosition = memberModel.MainPosition,

                Experience = memberModel.Experience,
                SkillPoints = memberModel.SkillPoints,
                RankPoints = memberModel.RankPoints,

                Energy = memberModel.Energy,
                MaxEnergy = memberModel.MaxEnergy,
                EnergyRegen = memberModel.EnergyRegen,
                MentalHealth = memberModel.MentalHealth,
                MaxMentalHealth = memberModel.MaxMentalHealth,
                MentalHealthRegen = memberModel.MentalHealthRegen,
                Teamplay = memberModel.Teamplay,
                MinTeamplay = memberModel.MinTeamplay,
                MaxTeamplay = memberModel.MaxTeamplay,

                Team = team,
                Positions = memberModel.Positions.Select(p => new Position
                {
                    Id = p.Id,
                    Type = p.Type,
                }).ToHashSet(),
                Traits = memberModel.Traits.Select(t => new MemberTrait
                {
                    Id = t.Id,
                    Type = t.Type,
                }).ToHashSet(),
            };
            UpdateStatesToCurrentTime(member);

            Context.Members.Add(member);
            Context.SaveChanges();

            memberModel.Id = member.Id;

            Context.Dispose();


            return memberModel;
        }

        public IEnumerable<MemberModel> GetAllMembers()
        {
            Context = new();
            var members = Context.Members
                .Include(m => m.Team)
                .AsNoTracking();
            foreach (var member in members) { UpdateStatesToCurrentTime(member); }
            Context.SaveChanges();

            var result = members.Select(m => new MemberModel(m)).ToList();

            Context.Dispose();

            return result;
        }

        public IEnumerable<MemberModel> GetAllFreeMembers()
        {
            Context = new();
            var members = Context.Members
                .Include(m => m.Team)
                .Where(m => m.Team == null);
            foreach (var member in members) { UpdateStatesToCurrentTime(member); }
            Context.SaveChanges();

            var result = members.Select(m => new MemberModel(m)).ToList();

            Context.Dispose();

            return result;
        }

        public IEnumerable<MemberModel> GetMembersOfTeam(int teamId)
        {
            Context = new();
            var members = Context.Members
                .Include(m => m.Team)
                .Where(m => m.TeamId == teamId);
            foreach (var member in members) { UpdateStatesToCurrentTime(member); }
            Context.SaveChanges();

            var result = members.Select(m => new MemberModel(m)).ToList();

            Context.Dispose();

            return result;
        }

        public MemberModel GetMember(int memberId)
        {
            Context = new();
            var member = Context.Members
                .Include(m => m.Team)
                .FirstOrDefault(m => m.Id == memberId)
                ?? throw new Exception($"Member does not exist!");
            UpdateStatesToCurrentTime(member);
            Context.SaveChanges();

            var result = new MemberModel(member);

            Context.Dispose();

            return result;
        }

        public IEnumerable<MemberModel> GetMembersInRank(PositionType type, int lowestRank, int highestRank)
        {
            Context = new();
            var members = Context.Members
                .Include(m => m.Team)
                .Where(m => m.MainPosition == type)
                .Where(m => m.RankPoints > lowestRank && m.RankPoints < highestRank);

            var result = new HashSet<MemberModel>();
            foreach (var member in members)
            {
                UpdateStatesToCurrentTime(member);
                result.Add(new MemberModel(member));
            }

            Context.SaveChanges();
            Context.Dispose();

            return result;
        }

        public MemberModel UpdateMember(MemberModel memberModel)
        {
            Context = new();
            var member = Context.Members
                .Include(m => m.Team)
                .FirstOrDefault(m => m.Id == memberModel.Id)
                ?? throw new Exception($"Member does not exist!");
            UpdateStatesToCurrentTime(member);

            memberModel.LastChanges = DateTime.Now;

            member.Name = memberModel.Name;
            member.Age = memberModel.Age;
            member.Attack = memberModel.Attack;
            member.Defense = memberModel.Defense;
            member.Intelligence = memberModel.Intelligence;
            member.ReactionSpeed = memberModel.ReactionSpeed;
            member.MentalPower = memberModel.MentalPower;
            member.MentalResistance = memberModel.MentalResistance;
            member.CreationDate = memberModel.CreationDate;
            member.MainPosition = memberModel.MainPosition;

            member.Experience = memberModel.Experience;
            member.SkillPoints = memberModel.SkillPoints;
            member.RankPoints = memberModel.RankPoints;

            member.Energy = memberModel.Energy;
            member.MaxEnergy = memberModel.MaxEnergy;
            member.EnergyRegen = memberModel.EnergyRegen;
            member.MentalHealth = memberModel.MentalHealth;
            member.MaxMentalHealth = memberModel.MaxMentalHealth;
            member.MentalHealthRegen = memberModel.MentalHealthRegen;
            member.Teamplay = memberModel.Teamplay;
            member.MinTeamplay = memberModel.MinTeamplay;
            member.MaxTeamplay = memberModel.MaxTeamplay;

            //REMOVING POSITIONS
            foreach (var position in member.Positions.ToList())
            {
                if (!memberModel.Positions.Select(p => p.Id).Contains(position.Id))
                {
                    Context.Positions.Remove(position);
                }
            }

            //ADDING NEW POSITIONS
            foreach (var positionModel in memberModel.Positions)
            {
                var position = member.Positions.FirstOrDefault(p => p.Id == positionModel.Id);

                if (position is null)
                {
                    position = new();

                    member.Positions.Add(position);
                    Context.Positions.Add(position);
                }

                position.Type = positionModel.Type;
            }

            //REMOVING TRAITS
            foreach (var trait in member.Traits.ToList())
            {
                if (!memberModel.Traits.Select(p => p.Id).Contains(trait.Id))
                {
                    Context.MemberTraits.Remove(trait);
                }
            }

            //ADDING NEW TRAITS
            foreach (var traitModel in memberModel.Traits)
            {
                var trait = member.Traits.FirstOrDefault(p => p.Id == traitModel.Id);

                if (trait is null)
                {
                    trait = new();

                    member.Traits.Add(trait);
                    Context.MemberTraits.Add(trait);
                }

                trait.Type = traitModel.Type;
            }

            Context.SaveChanges();
            Context.Entry(member).State = EntityState.Detached;
            Context.SaveChanges();
            Context.Dispose();

            return memberModel;
        }

        public bool DeleteMember(int memberId)
        {
            Context = new();
            var member = Context.Members.FirstOrDefault(m => m.Id == memberId)
                ?? throw new Exception($"Member does not exist!");

            Context.Members.Remove(member);
            Context.SaveChanges();
            Context.Dispose();

            return true;
        }

        public MemberModel UseSkillPoints(int memberId, UsingSkillPointsTypes usingSkillPointsTypes)
        {
            Context = new();
            var member = Context.Members.FirstOrDefault(m => m.Id == memberId)
                ?? throw new Exception($"Member does not exist!");
            UpdateStatesToCurrentTime(member);

            member = member.SkillPoints > 0
                ? UpStats(member, usingSkillPointsTypes)
                : throw new Exception($"{member.Name} hasn't enough skill point's. Skill point's count was: {member.SkillPoints}");

            Context.SaveChanges();

            var result = new MemberModel(member);

            Context.Dispose();

            return result;
        }

        private static Member UpStats(Member member, UsingSkillPointsTypes usingSkillPointsTypes)
        {
            member.SkillPoints--;

            switch (usingSkillPointsTypes)
            {
                case UsingSkillPointsTypes.Attack:
                    member.Attack++;
                    return member;
                case UsingSkillPointsTypes.Defense:
                    member.Defense++;
                    return member;
                case UsingSkillPointsTypes.MentalPower:
                    member.MentalPower++;
                    return member;
                case UsingSkillPointsTypes.MentalHealth:
                    member.MentalHealth += 5;
                    member.MaxMentalHealth += 5;
                    return member;
                case UsingSkillPointsTypes.Energy:
                    member.Energy++;
                    member.MaxEnergy++;
                    return member;
                case UsingSkillPointsTypes.Teamplay:
                    member.Teamplay++;
                    member.MinTeamplay++;
                    member.MaxTeamplay++;
                    return member;
                default:
                    return member;
            }
        }

        private static void UpdateStatesToCurrentTime(Member member)
        {
            var currentTime = DateTime.Now;

            member.Energy += (currentTime - member.LastChanges).TotalSeconds * member.EnergyRegen;
            member.Energy = member.Energy < 0 ? 0 : member.Energy;
            member.Energy = member.Energy > member.MaxEnergy ? member.MaxEnergy : member.Energy;

            member.MentalHealth += (currentTime - member.LastChanges).TotalSeconds * member.MentalHealthRegen;
            member.MentalHealth = member.MentalHealth < 0 ? 0 : member.MentalHealth;
            member.MentalHealth = member.MentalHealth > member.MaxMentalHealth ? member.MaxMentalHealth : member.MentalHealth;

            member.LastChanges = currentTime;
        }
    }
}
