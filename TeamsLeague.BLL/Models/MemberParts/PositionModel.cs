using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Models.MemberParts
{
    public class PositionModel
    {
        public int Id { get; set; }
        public PositionType Type { get; set; }


        public PositionModel() { }

        public PositionModel(Position position)
        {
            Id = position.Id;
            Type = position.Type;
        }
    }
}
