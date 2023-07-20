namespace TeamsLeague.DAL.Entities.MatchParts.KDA
{
    public class MatchEventAssist
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int EventId { get; set; }
        public virtual MatchEvent Event { get; set; }
        public virtual MatchSeat Seat { get; set; }
    }
}
