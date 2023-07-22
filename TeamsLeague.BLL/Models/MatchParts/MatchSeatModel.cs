using TeamsLeague.BLL.Models.MatchParts.KDA;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Models.MatchParts
{
    public class MatchSeatModel
    {
        public int Id { get; set; }
        public uint GainedRankPoints { get; set; }
        public uint GainedExperience { get; set; }
        public uint GainedGold { get; set; }
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
            GainedGold = place.GainedGold;
            Position = place.Position;
            Side = place.Side;

            Member = new MemberModel
            {
                Id = place.Member.Id,
                Name = place.Member.Name,
                Team = place.Member.Team is not null
                ? new TeamShortModel
                {
                    Id = place.Member.Team.Id,
                    Name = place.Member.Team.Name,
                } : null,
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
