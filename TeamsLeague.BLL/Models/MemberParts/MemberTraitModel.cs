using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Models.MemberParts
{
    public class MemberTraitModel
    {
        public int Id { get; set; }
        public MemberTraitType Type { get; set; }


        public MemberTraitModel() { }

        public MemberTraitModel(MemberTrait trait)
        {
            Id = trait.Id;
            Type = trait.Type;
        }
    }
}
