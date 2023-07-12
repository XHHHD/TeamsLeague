using TeamsLeague.BLL.Models.MemberParts;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMemberService
    {
        MemberModel CreateMember(MemberModel member);
        MemberModel ReadMember(int memberId);
        IEnumerable<MemberModel> GetMembers();
        MemberModel UpdateMember(MemberModel member);
        bool DeleteMember(int memberId);
    }
}
