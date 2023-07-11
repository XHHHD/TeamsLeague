using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Interfaces
{
    public interface ITeamBuilder
    {
        ITeamBuilder CreateBasicStats();
        ITeamBuilder GenerateMembers();
        ITeamBuilder GenerateMembers(PositionType type);
        ITeamBuilder AddTrait();
        ITeamBuilder AddTrait(TeamTaitType type);
        TeamModel Build();
    }
}
