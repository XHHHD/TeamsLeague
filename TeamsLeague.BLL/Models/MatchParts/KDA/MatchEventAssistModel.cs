using TeamsLeague.DAL.Entities.MatchParts.KDA;

namespace TeamsLeague.BLL.Models.MatchParts.KDA
{
    public class MatchEventAssistModel
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


        public MatchEventAssistModel() { }

        public MatchEventAssistModel(MatchEventAssist assist)
        {
            Id = assist.Id;
            DamageDone = assist.DamageDone;
            DamageTaken = assist.DamageTaken;
            DamageBlocked = assist.DamageBlocked;
            HealingDone = assist.HealingDone;
            ShieldsDone = assist.ShieldsDone;
            ControlTime = assist.ControlTime;
            ControlTaken = assist.ControlTaken;
            SlowsDone = assist.SlowsDone;
            SlowsTaken = assist.SlowsTaken;

            Event = new MatchEventModel
            {
                Id = assist.EventId,
            };
            Seat = new MatchSeatModel
            {
                Id = assist.SeatId,
            };
        }
    }
}