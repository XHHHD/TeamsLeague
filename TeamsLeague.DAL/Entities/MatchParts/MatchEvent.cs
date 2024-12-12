using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Entities.MatchParts.KDA;

namespace TeamsLeague.DAL.Entities.MatchParts
{
    public class MatchEvent
    {
        /// <summary>
        /// SECONDS
        /// </summary>
        public virtual Match Match { get; set; }

        public double TimeOfMoment { get; set; }
        public MatchEventType Type { get; set; }


        public virtual MatchEventKill Killer { get; set; }
        public virtual MatchEventDeath Dead { get; set; }
        public virtual ICollection<MatchEventAssist> Assistants { get; set; } = new List<MatchEventAssist>();


        public MapObjectType? ObjectType { get; set; }
        /// <summary>
        /// WHO EXACTLY DESTROYED OBJECT.
        /// </summary>
        public int? DestroyerSeatId { get; set; }
        /// <summary>
        /// ASSISTANTS OF DESTROYING.
        /// </summary>
        public virtual ICollection<MatchSeat> Destroyers { get; set; } = new HashSet<MatchSeat>();


        #region DB KEYS
        public int Id { get; set; }
        public int MatchId { get; set; }
        #endregion
    }
}
