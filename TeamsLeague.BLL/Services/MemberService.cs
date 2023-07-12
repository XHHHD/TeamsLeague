using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly GameDBContext _context;


        public MemberService()
        {
            _context = new();
        }


        public MemberModel CreateMember(MemberModel memberModel)
        {
            if (_context.Members.SingleOrDefault(m => m.Name == memberModel.Name) is not null)
            { throw new Exception($"Member with name - {memberModel.Name}, is already exist!"); }

            var member = new Member
            {
                Name = memberModel.Name,
                Age = memberModel.Age,
                Attack = memberModel.Attack,
                Defense = memberModel.Defense,
                CreationDate = memberModel.CreationDate,
                LastChanges = memberModel.LastChanges,

                Experience = memberModel.Experience,
                SkillPoints = memberModel.SkillPoints,
                RankPoints = memberModel.RankPoints,

                Energy = memberModel.Energy,
                MaxEnergy = memberModel.MaxEnergy,
                MentalPower = memberModel.MentalPower,
                MaxMentalPower = memberModel.MaxMentalPower,
                MentalHealth = memberModel.MentalHealth,
                MaxMentalHealth = memberModel.MaxMentalHealth,
            };


            _context.Members.Add(member);
            _context.SaveChanges();


            memberModel.Id = memberModel.Id;

            return memberModel;
        }

        public bool DeleteMember(int memberId)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == memberId)
                ?? throw new Exception($"Member does not exist!");

            _context.Members.Remove(member);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<MemberModel> GetMembers()
        {
            var members = _context.Members;

            var result = members.Select(m => new MemberModel
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
            });

            return result;
        }

        public MemberModel ReadMember(int memberId)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == memberId)
                ?? throw new Exception($"Member does not exist!");

            var result = new MemberModel
            {
                Id = member.Id,
                Name = member.Name,
                Age = member.Age,
                Attack = member.Attack,
                Defense = member.Defense,
                CreationDate = member.CreationDate,
                LastChanges = member.LastChanges,

                Experience = member.Experience,
                SkillPoints = member.SkillPoints,
                RankPoints = member.RankPoints,

                Energy = member.Energy,
                MaxEnergy = member.MaxEnergy,
                MentalPower = member.MentalPower,
                MaxMentalPower = member.MaxMentalPower,
                MentalHealth = member.MentalHealth,
                MaxMentalHealth = member.MaxMentalHealth,

                Team = member.Team != null
                ? new TeamModel
                {
                    Id = member.Team.Id,
                    Name = member.Team.Name,
                }
                : null,

                Positions = member.Positions.Select(p => new PositionModel
                {
                    Id = p.Id,
                    Type = p.Type,
                }).ToHashSet(),

                Traits = member.Traits.Select(t => new MemberTraitModel
                {
                    Id = t.Id,
                    Type = t.Type
                }).ToHashSet()
            };

            return result;
        }

        public MemberModel UpdateMember(MemberModel memberModel)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == memberModel.Id)
                ?? throw new Exception($"Member does not exist!");

            memberModel.LastChanges = DateTime.Now;

            member.Name = memberModel.Name;
            member.Age = memberModel.Age;
            member.Attack = memberModel.Attack;
            member.Defense = memberModel.Defense;
            member.CreationDate = memberModel.CreationDate;
            member.LastChanges = memberModel.LastChanges;

            member.Experience = memberModel.Experience;
            member.SkillPoints = memberModel.SkillPoints;
            member.RankPoints = memberModel.RankPoints;

            member.Energy = memberModel.Energy;
            member.MaxEnergy = memberModel.MaxEnergy;
            member.MentalPower = memberModel.MentalPower;
            member.MaxMentalPower = memberModel.MaxMentalPower;
            member.MentalHealth = memberModel.MentalHealth;
            member.MaxMentalHealth = memberModel.MaxMentalHealth;

            _context.SaveChanges();

            return memberModel;
        }
    }
}
