using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.BLL.Models.TeamParts
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public DateTime LastChanges { get; set; }

        public double Experience { get; set; }
        public int RankPoints { get; set; }
        public double Honor { get; set; }

        public double Energy { get; set; }
        public double MaxEnergy { get; set; }
        public double EnergyRegen { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public double HealthRegen { get; set; }
        public double Teamplay { get; set; }

        public UserModel? User { get; set; }
        public HashSet<MemberModel> Members { get; set; }
        public HashSet<TeamTraitModel> Traits { get; set; }


        public TeamModel() { }

        public TeamModel(Team team)
        {
            Id = team.Id;

            Name = team.Name;
            Image = team.Image;
            LastChanges = team.LastChanges;

            Experience = team.Experience;
            RankPoints = team.RankPoints;
            Honor = team.Honor;

            Energy = team.Energy;
            MaxEnergy = team.MaxEnergy;
            EnergyRegen = team.EnergyRegen;
            Health = team.Health;
            MaxHealth = team.MaxHealth;
            HealthRegen = team.HealthRegen;
            Teamplay = team.Teamplay;

            User = team.User is not null ? new UserModel(team.User) : null;
            Members = team.Members.Select(m => new MemberModel(m)).ToHashSet();
            Traits = team.Traits.Select(t => new TeamTraitModel(t)).ToHashSet();
        }
    }
}
