using TeamsLeague.BLL.Models.TeamParts;

namespace TeamsLeague.BLL.Models.MemberParts
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastChanges { get; set; }

        public double Experience { get; set; }
        public byte SkillPoints { get; set; }
        public int RankPoints { get; set; }

        public double Energy { get; set; }
        public double MaxEnergy { get; set; }
        public double MentalPower { get; set; }
        public double MaxMentalPower { get; set; }
        public double MentalHealth { get; set; }
        public double MaxMentalHealth { get; set; }

        public TeamModel? Team { get; set; }
        public HashSet<PositionModel> Positions { get; set; }
        public HashSet<MemberTraitModel> Traits { get; set; }
    }
}
