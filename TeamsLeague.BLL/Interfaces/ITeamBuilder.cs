using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Interfaces
{
    public interface ITeamBuilder
    {
        ITeamBuilder GenerateBasicStats(string teamName);
        /// <summary>
        /// Add random member to the team.
        /// </summary>
        ITeamBuilder GenerateMember();
        /// <summary>
        /// Add member with specific position to the team.
        /// </summary>
        /// <param name="type">Desired position type of member.</param>
        ITeamBuilder GenerateMember(PositionType type);
        /// <summary>
        /// Add to the team trait with random type, except trait types team already have, or do nothing.
        /// </summary>
        ITeamBuilder AddTrait();
        /// <summary>
        /// Add trait with specific type to the team.
        /// </summary>
        /// <param name="type">Desired trait type.</param>
        ITeamBuilder AddTrait(TeamTraitType type);
        TeamModel Build();
    }
}
