using TeamsLeague.BLL.Models.MatchParts.KDA;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Models.MatchParts
{
    public class MatchSeatModel
    {
        public int Id { get; set; }
        public int GainedRankPoints { get; set; }
        public int GainedExperience { get; set; }
        public PositionType Position { get; set; }
        public MatchSide Side { get; set; }

        public MemberModel Member { get; set; }
        public MatchModel Match { get; set; }
        public List<MatchEventKillModel> Kills { get; set; } = new();
        public List<MatchEventDeathModel> Dead { get; set; } = new();
        public List<MatchEventAssistModel> Assists { get; set; } = new();
        public HashSet<MatchEventModel> Destructions { get; set; } = new();


        public MatchSeatModel() { }

        public MatchSeatModel(MatchSeat place)
        {
            Id = place.Id;
            GainedRankPoints = place.GainedRankPoints;
            GainedExperience = place.GainedExperience;
            Position = place.Position;
            Side = place.Side;

            Member = new MemberModel
            {
                Id = place.Member.Id,
                Name = place.Member.Name,
            };
            Match = new MatchModel
            {
                Id = place.Match.Id,
            };
            Kills = place.Kills.Select(k => new MatchEventKillModel(k)).ToList();
            Dead = place.Dead.Select(d => new MatchEventDeathModel(d)).ToList();
            Assists = place.Assists.Select(a => new MatchEventAssistModel(a)).ToList();
            Destructions = place.Destructions.Select(d => new MatchEventModel(d)).ToHashSet();
        }
    }
}
