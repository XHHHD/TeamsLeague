using TeamsLeague.BLL.Models.TeamParts;

namespace TeamsLeague.BLL.Interfaces
{
    public interface ITeamService
    {
        TeamModel CreateTeam(TeamModel team);
        TeamModel GetTeam(int teamId);
        IEnumerable<TeamModel> GetTeamsInRank(int rankFrom, int rankTo);
        IEnumerable<TeamModel> GetAllTeams();
        TeamModel UpdateTeam(TeamModel team);
        bool DeleteTeam(int teamId);
        bool IsTeamNameIsFree(string teamName);
        bool AddMemberToTheTeam(int teamId, int memberId);
    }
}
