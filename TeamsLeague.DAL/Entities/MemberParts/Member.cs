using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class Member
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double MovePoints { get; set; }
        public double Experience { get; set; }
        public double MentalHealth { get; set; }
        public virtual Team? Team { get; set; }
        public virtual ICollection<Position> Positions { get; set; } = new HashSet<Position>();
        public virtual ICollection<MemberTrait> Traits { get; set; } = new HashSet<MemberTrait>();

        #region DB KEYS
        public int Id { get; set; }
        public int? TeamId { get; set; }
        #endregion
    }
}
