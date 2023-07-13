using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.DAL.Entities.TeamParts
{
    public class Team
    {
        #region BASIC STATS
        public string Name { get; set; }
        public string? Image { get; set; }
        public DateTime LastChanges { get; set; }

        public double Experience { get; set; }
        public int RankPoints { get; set; }
        public double Honor { get; set; }

        public double Energy { get; set; }
        public double MaxEnergy { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public double Teamplay { get; set; }
        #endregion


        #region PROXY STATS
        public virtual User? User { get; set; }
        public virtual ICollection<Member> Members { get; set; } = new HashSet<Member>();
        public virtual ICollection<TeamTrait> Traits { get; set; } = new HashSet<TeamTrait>();
        #endregion


        #region DB KEYS
        public int Id { get; set; }
        #endregion
    }
}
