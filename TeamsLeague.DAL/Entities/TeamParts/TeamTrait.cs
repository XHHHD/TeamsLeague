using TeamsLeague.DAL.Constants;

namespace TeamsLeague.DAL.Entities.TeamParts
{
    public class TeamTrait
    {
        public TeamTraitType Type { get; set; }
        public virtual Team Team { get; set; }


        #region DB KEYS
        public int Id { get; set; }
        public int TeamId { get; set; }
        #endregion
    }
}
