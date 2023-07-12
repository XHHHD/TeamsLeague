using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMemberBuilder
    {
        IMemberBuilder GenerateBasicStats(string memberName);
        /// <summary>
        /// Add to the member position with random type, except position types he already have, or do nothing.
        /// </summary>
        IMemberBuilder AddPosition();
        /// <summary>
        /// Add to the member position with specific type, except position types he already have.
        /// </summary>
        IMemberBuilder AddPosition(PositionType type);
        /// <summary>
        /// Add to the member trait with random type, except trait types member already have, or do nothing.
        /// </summary>
        IMemberBuilder AddTrait();
        /// <summary>
        /// Add to the member trait with specific type, except trait types member already have.
        /// </summary>
        IMemberBuilder AddTrait(MemberTraitType type);
        MemberModel Build();
    }
}
