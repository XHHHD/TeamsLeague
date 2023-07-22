using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.BLL.Models.MatchParts.MatchMap;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Services
{
    public class MatchService : IMatchService
    {
        private readonly GameDBContext _context;


        private Match CurrentMatch { get; set; }
        private MatchMapVM Map { get; set; }
        private HashSet<MapObject> DestroyedTurrets { get; set; }


        public MatchService(GameDBContext context)
        {
            _context = context;

            CurrentMatch = new();
            Map = new();
            DestroyedTurrets = new();
        }


        public MatchModel InitiateMatch(int matchId)
        {
            CurrentMatch = _context.Matches.FirstOrDefault(m => m.Id == matchId)
                ?? throw new Exception("Match wasn't found!");

            if(CurrentMatch.IsItEnded) { throw new Exception("Match is already ended!"); }

            StartMatch();

            CurrentMatch.IsItEnded = true;
            CurrentMatch.Date = DateTime.Now;

            _context.SaveChanges();

            return new MatchModel(CurrentMatch);
        }

        private void StartMatch()
        {
            EarlyGame();

            if (!Map.LeftAncientNest.isDestroyed && !Map.RightAncientNest.isDestroyed)
            {
                MidGame();
            }

            if (!Map.LeftAncientNest.isDestroyed && !Map.RightAncientNest.isDestroyed)
            {
                LateGame();
            }
        }

        private void EarlyGame()
        {
            do
            {
            } while (DestroyedTurrets.Where(t => t.Type == MapObjectType.Tier1).Count() < 3);
        }

        private void MidGame()
        {
            do
            {
            } while (DestroyedTurrets.Where(t => t.Type == MapObjectType.Tier3).Count() < 3);
        }

        private void LateGame()
        {
            do
            {
            } while (!Map.LeftAncientNest.isDestroyed && !Map.RightAncientNest.isDestroyed);
        }
    }
}
