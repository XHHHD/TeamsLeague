using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMemberBuilder
    {
        IMemberBuilder GenerateBasicStats();
        IMemberBuilder AddPosition();
        IMemberBuilder AddPosition(PositionType type);
        IMemberBuilder AddTrait();
        IMemberBuilder AddTrait(MemberTraitType type);
        MemberModel Build();
    }
}
