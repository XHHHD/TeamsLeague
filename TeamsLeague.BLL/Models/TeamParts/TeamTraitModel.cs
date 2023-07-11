using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Models.TeamParts
{
    public class TeamTraitModel
    {
        public int Id { get; set; }
        public TeamTraitType Type { get; set; }
        public TeamModel Team { get; set; }
    }
}
