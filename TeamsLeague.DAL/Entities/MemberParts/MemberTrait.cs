using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class MemberTrait
    {
        public MemberTraitType Type { get; set; }
        public virtual Member Member { get; set; }


        #region DB KEYS
        public int Id { get; set; }
        public int MemberId { get; set; }
        #endregion
    }
}
