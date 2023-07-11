using TeamsLeague.DAL.Constants;

namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class Position
    {
        public PositionType Type { get; set; }
        public virtual Member Member { get; set; }


        #region DB KEYS
        public int Id { get; set; }
        public int MemberId { get; set; }
        #endregion
    }
}
