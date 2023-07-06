using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Models.MemberParts
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<MemberPositionType> Positions { get; set; }
        public TeamModel? Team { get; set; }
    }
}
