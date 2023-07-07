using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Models.TeamParts
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<MemberModel> Members { get; set; }
        public UserModel? User { get; set; }


        public TeamModel() { }

        public TeamModel(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Members = team.Members.Select(m => new MemberModel()
            {
                Id = m.Id,
                Name = m.Name,
                Team = new TeamModel(m.Team),
            }).ToHashSet();
        }
    }
}
