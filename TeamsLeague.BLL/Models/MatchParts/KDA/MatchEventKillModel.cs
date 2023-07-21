using TeamsLeague.DAL.Entities.MatchParts.KDA;

namespace TeamsLeague.BLL.Models.MatchParts.KDA
{
    public class MatchEventKillModel
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


        public MatchEventKillModel() { }

        public MatchEventKillModel(MatchEventKill kill)
        {
            Id = kill.Id;
            DamageDone = kill.DamageDone;
            DamageTaken = kill.DamageTaken;
            DamageBlocked = kill.DamageBlocked;
            HealingDone = kill.HealingDone;
            ShieldsDone = kill.ShieldsDone;
            ControlTime = kill.ControlTime;
            ControlTaken = kill.ControlTaken;
            SlowsDone = kill.SlowsDone;
            SlowsTaken = kill.SlowsTaken;

            Event = new MatchEventModel
            {
                Id = kill.EventId,
            };
            Seat = new MatchSeatModel
            {
                Id = kill.SeatId,
            };
        }
    }
}