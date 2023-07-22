using TeamsLeague.BLL.Models.MatchParts;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMatchService
    {
        MatchModel InitiateMatch(int matchId);
    }
}
