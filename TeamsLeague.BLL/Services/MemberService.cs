using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly GameDBContext _context;


        public MemberService()
        {
            _context = new();
        }


        public MemberModel CreateMember(MemberModel memberModel)
        {
            var member = new Member()
            {
            };
            //
            return memberModel;
        }

        public bool DeleteMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberModel> GetMembers()
        {
            throw new NotImplementedException();
        }

        public MemberModel ReadMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public MemberModel UpdateMember(MemberModel memberModel)
        {
            throw new NotImplementedException();
        }
    }
}
