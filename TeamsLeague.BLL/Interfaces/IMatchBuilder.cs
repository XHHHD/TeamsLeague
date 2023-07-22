using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMatchBuilder
    {
        IMatchBuilder SetupSoloRank(int memberId);
        IMatchBuilder SetupSoloRank(int memberId, PositionType type);
        IMatchBuilder SetupCommonRank(Dictionary<int, PositionType> membersPositions);
        IMatchBuilder SetupTeamRank(int teamId);
        IMatchBuilder SetupScrimmages(int teamAId, int teamBId);
        IMatchBuilder AddResults();
        MatchModel GetMatch();
    }
}
