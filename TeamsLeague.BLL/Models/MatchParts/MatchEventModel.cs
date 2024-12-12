using TeamsLeague.BLL.Models.MatchParts.KDA;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Models.MatchParts
{
    public class MatchEventModel
    {
        public int Id { get; set; }
        /// <summary>
        /// SECONDS
        /// </summary>
        public MatchModel Match { get; set; }

        public double TimeOfMoment { get; set; }
        public MatchEventType Type { get; set; }


        //EVENT KILL
        public MatchEventKillModel? Killers { get; set; }
        public MatchEventDeathModel? Dead { get; set; }
        public List<MatchEventAssistModel> Assistants { get; set; } = new();
        /// <summary>
        /// WHO EXACTLY DESTROYED OBJECT.
        /// </summary>


        //EVENT OBJECT DESTRUCTION
        public MapObjectType? DestroyedObjectType { get; set; }
        public int? DestroyerSeatId { get; set; }
        public MatchSeatModel? DestroyerSeat { get; set; }
        /// <summary>
        /// ASSISTANTS OF DESTROYING.
        /// </summary>
        public HashSet<MatchSeatModel> Destroyers { get; set; } = new();


        public MatchEventModel() { }

        public MatchEventModel(MatchEvent matchEvent)
        {
            Id = matchEvent.Id;
            Match = new MatchModel
            {
                Id = matchEvent.MatchId,
            };

            TimeOfMoment = matchEvent.TimeOfMoment;
            Type = matchEvent.Type;


            Killers = matchEvent.Killer is not null ? new MatchEventKillModel(matchEvent.Killer) : null;
            Dead = matchEvent.Dead is not null ? new MatchEventDeathModel(matchEvent.Dead) : null;
            Assistants = matchEvent.Assistants is not null ? matchEvent.Assistants.Select(a => new MatchEventAssistModel(a)).ToList() : new List<MatchEventAssistModel>();


            DestroyedObjectType = matchEvent.ObjectType;
            DestroyerSeatId = matchEvent.DestroyerSeatId;
            Destroyers = matchEvent.Destroyers.Select(d => new MatchSeatModel(d)).ToHashSet();
        }

        public MatchEventModel(MatchEvent matchEvent, MatchSeatModel destroyerSeat) : this(matchEvent)
        {
            DestroyerSeat = destroyerSeat;
        }
    }
}