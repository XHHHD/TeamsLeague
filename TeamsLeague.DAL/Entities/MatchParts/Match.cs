using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Entities.MatchParts
{
    public class Match
    {
        /// <summary>
        /// SECONDS
        /// </summary>
        public double Duration { get; set; }
        public DateTime Date { get; set; }
        public MatchSide? Winer { get; set; }
        public bool IsItEnded { get; set; } = false;

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
        public virtual ICollection<MatchSeat> Seats { get; set; } = new HashSet<MatchSeat>();
        public virtual ICollection<MatchEvent> Events { get; set; } = new List<MatchEvent>();


        #region DB KEYS
        public int Id { get; set; }
        public int TeamId { get; set; }
        #endregion
    }
}
