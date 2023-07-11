using TeamsLeague.BLL.Models.TeamParts;

namespace TeamsLeague.BLL.Models.MemberParts
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double MovePoints { get; set; }
        public double Experience { get; set; }
        public double MentalHealth { get; set; }
        public TeamModel? Team { get; set; }
        public HashSet<PositionModel> Positions { get; set; }
        public HashSet<MemberTrait> MemberTraits { get; set; }
    }
}
