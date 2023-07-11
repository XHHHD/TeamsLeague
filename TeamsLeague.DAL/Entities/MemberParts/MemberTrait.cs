namespace TeamsLeague.DAL.Entities.MemberParts
{
    public class MemberTrait
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Member Member { get; set; }


        public int Id { get; set; }
        public int MemberId { get; set; }
    }
}
