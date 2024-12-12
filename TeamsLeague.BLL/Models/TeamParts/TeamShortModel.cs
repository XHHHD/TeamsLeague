using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Models.TeamParts
{
    public class TeamShortModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public TeamShortModel() { }

        public TeamShortModel(Team team)
        {
            Id = team.Id;
            Name = team.Name;
        }

        public TeamShortModel(TeamModel model)
        {
            Id = model.Id;
            Name = model.Name;
        }
    }
}
