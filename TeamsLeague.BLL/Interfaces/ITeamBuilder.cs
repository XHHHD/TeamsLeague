using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Interfaces
{
    public interface ITeamBuilder
    {
        ITeamBuilder GenerateBasicStats();
        ITeamBuilder GenerateBasicStats(string teamName);
        ITeamBuilder GenerateMember();
        ITeamBuilder GenerateMember(PositionType type);
        ITeamBuilder AddTrait();
        ITeamBuilder AddTrait(TeamTraitType type);
        TeamModel Build();
    }
}
