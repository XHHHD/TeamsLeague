using TeamsLeague.BLL.Models.MatchParts;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMatchService
    {
        MatchModel GetMatch(int matchId);
        MatchModel InitiateMatch(int matchId);
    }
}
