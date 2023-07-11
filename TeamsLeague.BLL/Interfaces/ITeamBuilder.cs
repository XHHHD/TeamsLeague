using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Interfaces
{
    public interface ITeamBuilder
    {
        ITeamBuilder GenerateBasicStats();
        ITeamBuilder GenerateMembers();
        ITeamBuilder GenerateMembers(PositionType type);
        ITeamBuilder AddTrait();
        ITeamBuilder AddTrait(TeamTraitType type);
        TeamModel Build();
    }
}
