using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.MatchParts.KDA;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.DAL.Entities.MatchParts
{
    public class MatchSeat
    {
        public int GainedRankPoints { get; set; }
        public int GainedExperience { get; set; }
        public uint DamageDone { get; set; }
        public uint DamageTaken { get; set; }
        public uint DamageBlocked { get; set; }
        public uint HealingDone { get; set; }
        public uint ShieldsDone { get; set; }
        public uint ControlTime { get; set; }
        public uint ControlTaken { get; set; }
        public uint SlowsDone { get; set; }
        public uint SlowsTaken { get; set; }
        public PositionType Position { get; set; }
        public MatchSide Side { get; set; }

        public virtual Member Member { get; set; }
        public virtual Match Match { get; set; }
        public virtual ICollection<MatchEventKill> Kills { get; set; } = new List<MatchEventKill>();
        public virtual ICollection<MatchEventDeath> Dead { get; set; } = new List<MatchEventDeath>();
        public virtual ICollection<MatchEventAssist> Assists { get; set; } = new List<MatchEventAssist>();
        public virtual ICollection<MatchEvent> Destructions { get; set; } = new List<MatchEvent>();


        #region DB KEYS
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MatchId { get; set; }
        #endregion
    }
}
