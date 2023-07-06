using TeamsLeague.BLL.Models.MemberParts;

namespace TeamsLeague.BLL.Models.TeamParts
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<MemberModel> Members { get; set; }
        public UserModel? User { get; set; }
    }
}
