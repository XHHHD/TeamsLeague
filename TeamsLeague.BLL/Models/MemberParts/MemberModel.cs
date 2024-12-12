using TeamsLeague.BLL.Models.MatchParts;
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
        public double Mobility { get; set; }
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
        public HashSet<PositionModel> Positions { get; set; } = new();
        public HashSet<MemberTraitModel> Traits { get; set; } = new();
        public HashSet<MatchSeatModel> SeatsHistory { get; set; } = new();
        public HashSet<GameCharacterModel> CharsPool { get; set; } = new();


        public MemberModel() { }

        public MemberModel(Member member)
        {
            Id = member.Id;

            Name = member.Name;
            Age = member.Age;
            Attack = Math.Round(member.Attack);
            Defense = Math.Round(member.Defense);
            Intelligence = Math.Round(member.Intelligence);
            ReactionSpeed = Math.Round(member.ReactionSpeed);
            Mobility = Math.Round(member.Mobility);
            MentalPower = Math.Round(member.MentalPower);
            MentalResistance = Math.Round(member.MentalResistance);
            CreationDate = member.CreationDate;
            LastChanges = member.LastChanges;
            MainPosition = member.MainPosition;
            PlayStyle = member.PlayStyle;

            Experience = member.Experience;
            SkillPoints = member.SkillPoints;
            RankPoints = member.RankPoints;

            Energy = Math.Round(member.Energy);
            MaxEnergy = Math.Round(member.MaxEnergy);
            EnergyRegen = Math.Round(member.EnergyRegen);
            MentalHealth = Math.Round(member.MentalHealth);
            MaxMentalHealth = Math.Round(member.MaxMentalHealth);
            MentalHealthRegen = Math.Round(member.MentalHealthRegen);
            Teamplay = Math.Round(member.Teamplay);
            MinTeamplay = Math.Round(member.MinTeamplay);
            MaxTeamplay = Math.Round(member.MaxTeamplay);

            Team = member.Team is not null ? new TeamShortModel(member.Team) : null;
            Positions = member.Positions.Select(p => new PositionModel(p)).ToHashSet();
            Traits = member.Traits.Select(t => new MemberTraitModel(t)).ToHashSet();
            SeatsHistory = member.SeatsHistory.Select(s => new MatchSeatModel(s)).ToHashSet();
            CharsPool = member.CharPool.Select(c => new GameCharacterModel(c)).ToHashSet();
        }
    }
}
