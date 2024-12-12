namespace TeamsLeague.DAL.Entities.MatchParts.KDA
{
    public class MatchEventKill
    {
        public uint DamageDone { get; set; }
        public uint DamageTaken { get; set; }
        public uint DamageBlocked { get; set; }
        public uint HealingDone { get; set; }
        public uint ShieldsDone { get; set; }
        public uint ControlTime { get; set; }
        public uint ControlTaken { get; set; }
        public uint SlowsDone { get; set; }
        public uint SlowsTaken { get; set; }

        public virtual MatchEvent Event { get; set; }
        public virtual MatchSeat Seat { get; set; }


        #region DB KEYS
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int EventId { get; set; }
        #endregion
    }
}
