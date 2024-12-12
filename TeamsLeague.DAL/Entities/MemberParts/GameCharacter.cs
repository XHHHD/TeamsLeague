using TeamsLeague.DAL.Constants.Match.GamePerson;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class GameCharacter
    {
        public double Experience { get; set; }
        public GameCharType Type { get; set; }
        public virtual Member Member { get; set; }
        public virtual ICollection<MatchSeat> Seats { get; set; } = new HashSet<MatchSeat>();


        #region DB KEYS
        public int Id { get; set; }
        public int MemberId { get; set; }
        #endregion
    }
}
