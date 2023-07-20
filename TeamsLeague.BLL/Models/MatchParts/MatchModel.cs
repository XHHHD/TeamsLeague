using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Models.MatchParts
{
    public class MatchModel
    {
        public int Id { get; set; }
        public IEnumerable<MatchPlaceModel> TeamA { get; set; }
        public IEnumerable<MatchPlaceModel> TeamB { get; set; }


        public MatchModel() { }


        public MatchModel(Match match)
        {
            Id = match.Id;
            TeamA = match.Seats.Where(m => m.Side == MatchSide.A).Select(m => new MatchPlaceModel(m)).ToHashSet();
            TeamB = match.Seats.Where(m => m.Side == MatchSide.B).Select(m => new MatchPlaceModel(m)).ToHashSet();
        }
    }
}
