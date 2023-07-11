using TeamsLeague.DAL.Constants;

namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class Position
    {
        public string Name { get; set; }
        public PositionType Type { get; set; }
        public Member Member { get; set; }


        public int Id { get; set; }
        public int MemberId { get; set; }
    }
}
