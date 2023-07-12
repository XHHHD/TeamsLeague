using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Entities;

namespace TeamsLeague.BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeamModel Team { get; set; }
    }
}
