using TeamsLeague.BLL.Models.MatchParts.MatchMap;
using TeamsLeague.DAL.Constants.GamePersons;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Services.Generators
{
    public class LogGenerator
    {
        private Match CurrentMatch { get; set; }
        private MatchMapVM Map { get; set; }


        public LogGenerator(Match match, MatchMapVM mapVM)
        {
            CurrentMatch = match;
            Map = mapVM;
        }

        public void MatchStart() =>
             Console.WriteLine($"╔════════════════════════════════════════════╗\n" +
                                "║              MATCH IS STARTED!             ║\n" +
                                "╚════════════════════════════════════════════╝");
        public void MatchEnd() => Console.WriteLine();

        public void MatchError() => Console.WriteLine();

        public void EventKill(GameChar killer, GameChar dead, IEnumerable<GameChar> assistants) => CurrentMatch.MatchLog += $"{GetTimeMatch()}: {killer.Name} killed {dead.Name}. Assistants: {string.Join(", ", assistants.Select(a => a.Name))}.\n";

        private string GetTimeMatch() => Math.Round(CurrentMatch.Duration / 3600).ToString() + "h." + Math.Round(CurrentMatch.Duration % 3600 / 60).ToString() + "m." + (CurrentMatch.Duration % 60).ToString() + "s.";
    }
}
