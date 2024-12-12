using TeamsLeague.DAL.Entities.MatchParts.KDA;

namespace TeamsLeague.BLL.Models.MatchParts.KDA
{
    public class MatchEventDeathModel
    {
        public int Id { get; set; }
        public uint DamageDone { get; set; }
        public uint DamageTaken { get; set; }
        public uint DamageBlocked { get; set; }
        public uint HealingDone { get; set; }
        public uint ShieldsDone { get; set; }
        public uint ControlTime { get; set; }
        public uint ControlTaken { get; set; }
        public uint SlowsDone { get; set; }
        public uint SlowsTaken { get; set; }

        public MatchEventModel Event { get; set; }
        public MatchSeatModel Seat { get; set; }


        public MatchEventDeathModel() { }

        public MatchEventDeathModel(MatchEventDeath death)
        {
            Id = death.Id;
            DamageDone = death.DamageDone;
            DamageTaken = death.DamageTaken;
            DamageBlocked = death.DamageBlocked;
            HealingDone = death.HealingDone;
            ShieldsDone = death.ShieldsDone;
            ControlTime = death.ControlTime;
            ControlTaken = death.ControlTaken;
            SlowsDone = death.SlowsDone;
            SlowsTaken = death.SlowsTaken;

            Event = new MatchEventModel
            {
                Id = death.EventId,
            };
            Seat = new MatchSeatModel
            {
                Id = death.SeatId,
            };
        }
    }
}