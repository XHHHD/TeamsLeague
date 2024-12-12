using TeamsLeague.DAL.Constants;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Models.TeamParts
{
    public class TeamTraitModel
    {
        public int Id { get; set; }
        public TeamTraitType Type { get; set; }


        public TeamTraitModel() { }

        public TeamTraitModel(TeamTrait trait)
        {
            Id = trait.Id;
            Type = trait.Type;
        }
    }
}
