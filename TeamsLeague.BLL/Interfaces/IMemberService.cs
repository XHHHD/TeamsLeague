using TeamsLeague.BLL.Models;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMemberService
    {
        MemberModel CreateMember(MemberModel member);
        MemberModel GetMember(int memberId);
        IEnumerable<MemberModel> GetAllMembers();
        IEnumerable<MemberModel> GetAllFreeMembers();
        IEnumerable<MemberModel> GetMembersOfTeam(int teamId);
        IEnumerable<MemberModel> GetMembersInRank(PositionType type, int lowestRank, int highestRank);
        MemberModel UpdateMember(MemberModel member);
        bool DeleteMember(int memberId);
        MemberModel UseSkillPoints(int memberId, UsingSkillPointsTypes usingSkillPointsTypes);
    }
}
