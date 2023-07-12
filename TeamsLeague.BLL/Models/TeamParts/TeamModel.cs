using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Models.TeamParts
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }

        public double Experience { get; set; }
        public int RankPoints { get; set; }
        public double Honor { get; set; }

        public double Energy { get; set; }
        public double MaxEnergy { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public double Teamplay { get; set; }

        public UserModel? User { get; set; }
        public HashSet<MemberModel> Members { get; set; }
        public HashSet<TeamTraitModel> Traits { get; set; }


        public TeamModel() { }

        public TeamModel(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Members = team.Members.Select(m => new MemberModel
            {
                Id = m.Id,
                Name = m.Name,
                Team = new TeamModel(team),
            }).ToHashSet();
            Traits = team.Traits.Select(tt => new TeamTraitModel
            {
                Id = tt.Id,
                Type = tt.Type,
            }).ToHashSet();
        }
    }
}
