using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Models.MatchParts
{
    public class MatchModel
    {
        public int Id { get; set; }
        /// <summary>
        /// SECONDS
        /// </summary>
        public double Duration { get; set; }
        public DateTime? Date { get; set; }
        public MatchSide? Winer { get; set; }
        public TypeOfMatch Type { get; set; }
        public bool IsItEnded { get; set; } = false;

        public HashSet<TeamShortModel> Teams { get; set; } = new();
        public List<MatchEventModel> Events { get; set; } = new();
        public HashSet<MatchSeatModel> TeamA { get; set; } = new();
        public HashSet<MatchSeatModel> TeamB { get; set; } = new();


        public MatchModel() { }


        public MatchModel(Match match)
        {
            Id = match.Id;
            Duration = match.Duration;
            Date = match.Date;
            Winer = match.Winer;
            Type = match.Type;
            IsItEnded = match.IsItEnded;

            Teams = match.Teams.Select(t => new TeamShortModel(t)).ToHashSet();
            Events = match.Events.Select(e => new MatchEventModel(e)).ToList();
            TeamA = match.Seats.Where(m => m.Side == MatchSide.A).Select(m => new MatchSeatModel(m)).ToHashSet();
            TeamB = match.Seats.Where(m => m.Side == MatchSide.B).Select(m => new MatchSeatModel(m)).ToHashSet();
        }
    }
}
