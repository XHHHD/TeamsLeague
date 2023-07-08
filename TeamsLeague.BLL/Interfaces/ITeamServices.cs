using TeamsLeague.BLL.Models.TeamParts;

namespace TeamsLeague.BLL.Interfaces
{
    public interface ITeamServices
    {
        TeamModel CreateTeam(TeamModel team);
        TeamModel ReadTeam(int teamId);
        IEnumerable<TeamModel> GetTeams();
        TeamModel UpdateTeam(TeamModel team);
        bool DeleteTeam(int teamId);
    }
}
