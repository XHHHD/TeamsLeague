using TeamsLeague.DAL.Constants;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class Member
    {
        public string Name { get; set; }
        //public virtual ICollection<MemberPositionType> Positions { get; set; } = new HashSet<MemberPositionType>();
        public virtual Team? Team { get; set; }

        #region DB KEYS
        public int Id { get; set; }
        public int? TeamId { get; set; }
        #endregion
    }
}
