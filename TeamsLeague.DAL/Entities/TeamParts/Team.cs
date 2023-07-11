using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.DAL.Entities.TeamParts
{
    public class Team
    {
        public string Name { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Member> Members { get; set; } = new HashSet<Member>();
        public virtual ICollection<TeamTrait> Traits { get; set; } = new HashSet<TeamTrait>();


        #region DB KEYS
        public int Id { get; set; }
        #endregion
    }
}
