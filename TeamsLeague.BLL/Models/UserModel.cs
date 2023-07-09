using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Entities;

namespace TeamsLeague.BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeamModel Team { get; set; }


        public UserModel() { }

        public UserModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Team = new TeamModel(user.Team);
        }
    }
}
