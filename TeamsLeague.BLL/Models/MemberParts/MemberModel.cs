using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Models.MemberParts
{
    public class MemberModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double Intelligence { get; set; }
        public double ReactionSpeed { get; set; }
        public double MentalPower { get; set; }
        public double MentalResistance { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastChanges { get; set; }
        public PositionType MainPosition { get; set; }
        public PlaystyleType PlayStyle { get; set; }

        public double Experience { get; set; }
        public byte SkillPoints { get; set; }
        public int RankPoints { get; set; }

        public double Energy { get; set; }
        public double MaxEnergy { get; set; }
        public double EnergyRegen { get; set; }
        public double MentalHealth { get; set; }
        public double MaxMentalHealth { get; set; }
        public double MentalHealthRegen { get; set; }
        public double Teamplay { get; set; }
        public double MinTeamplay { get; set; }
        public double MaxTeamplay { get; set; }

        public TeamShortModel? Team { get; set; }
        public HashSet<PositionModel> Positions { get; set; }
        public HashSet<MemberTraitModel> Traits { get; set; }


        public MemberModel() { }

        public MemberModel(Member member)
        {
            Id = member.Id;

            Name = member.Name;
            Age = member.Age;
            Attack = member.Attack;
            Defense = member.Defense;
            Intelligence = member.Intelligence;
            ReactionSpeed = member.ReactionSpeed;
            MentalPower = member.MentalPower;
            MentalResistance = member.MentalResistance;
            CreationDate = member.CreationDate;
            LastChanges = member.LastChanges;
            MainPosition = member.MainPosition;
            PlayStyle = member.PlayStyle;

            Experience = member.Experience;
            SkillPoints = member.SkillPoints;
            RankPoints = member.RankPoints;

            Energy = member.Energy;
            MaxEnergy = member.MaxEnergy;
            EnergyRegen = member.EnergyRegen;
            MentalHealth = member.MentalHealth;
            MaxMentalHealth = member.MaxMentalHealth;
            MentalHealthRegen = member.MentalHealthRegen;
            Teamplay = member.Teamplay;
            MinTeamplay = member.MinTeamplay;
            MaxTeamplay = member.MaxTeamplay;

            Team = member.Team is not null ? new TeamShortModel(member.Team) : null;
            Positions = member.Positions.Select(p => new PositionModel(p)).ToHashSet();
            Traits = member.Traits.Select(t => new MemberTraitModel(t)).ToHashSet();
        }
    }
}
