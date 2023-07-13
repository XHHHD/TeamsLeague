using TeamsLeague.BLL.Models.MemberParts;

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
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public double Teamplay { get; set; }

        public UserModel? User { get; set; }
        public HashSet<MemberModel> Members { get; set; }
        public HashSet<TeamTraitModel> Traits { get; set; }
    }
}
