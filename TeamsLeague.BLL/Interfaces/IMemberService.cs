﻿using TeamsLeague.BLL.Models.MemberParts;

namespace TeamsLeague.BLL.Interfaces
{
    public interface IMemberService
    {
        MemberModel CreateMember(MemberModel member);
        MemberModel GetMember(int memberId);
        IEnumerable<MemberModel> GetAllMembers();
        MemberModel UpdateMember(MemberModel member);
        bool DeleteMember(int memberId);
    }
}
