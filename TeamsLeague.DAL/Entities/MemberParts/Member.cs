using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class Member
    {
        #region BASIC STATS
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
        #endregion

        #region PROXY STATS
        public virtual Team? Team { get; set; }
        public virtual ICollection<Position> Positions { get; set; } = new HashSet<Position>();
        public virtual ICollection<MemberTrait> Traits { get; set; } = new HashSet<MemberTrait>();
        #endregion

        #region DB KEYS
        public int Id { get; set; }
        public int? TeamId { get; set; }
        #endregion
    }
}
