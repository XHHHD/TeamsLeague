using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Models.MatchParts
{
    public class MatchPlaceModel
    {
        public int Id { get; set; }
        public uint Kills { get; set; }
        public uint Deaths { get; set; }
        public uint Assists { get; set; }
        public PositionType Position { get; set; }
        public MatchSide Side { get; set; }

        public virtual MemberModel Member { get; set; }
        public virtual MatchModel Match { get; set; }


        public MatchPlaceModel() { }

        public MatchPlaceModel(MatchSeat place)
        {
            Id = place.Id;
            Position = place.Position;
            Side = place.Side;
            Member = new MemberModel
            {
                Id = place.Member.Id,
                Name = place.Member.Name,
            };
            Match = new MatchModel
            {
                Id = place.Match.Id,
            };
        }
    }
}
