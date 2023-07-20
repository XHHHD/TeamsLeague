using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMatchService
    {
        MatchModel GetSoloRank(int memberId);
        MatchModel GetSoloRank(int memberId, PositionType type);
        MatchModel GetCommonRank(Dictionary<int, PositionType> membersPositions);
        MatchModel GetTeamMatch(int teamId);
    }
}
