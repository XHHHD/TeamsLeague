using TeamsLeague.DAL.Constants.Match;

namespace TeamsLeague.BLL.Models.MatchParts.MatchMap
{
    public class MatchMapVM
    {
        public const double maximumGoldIncomeInMinLane = 100.0;
        public const double maximumGoldIncomeInMinJungle = 80.0;
        public const double maximumExpIncomeInMinLane = 100.0;
        public const double maximumExpIncomeInMinJungle = 80.0;


        //LEFT SIDE OF MAP
        public MapObject LeftTopT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.A, MapLane.Top);
        public MapObject LeftTopT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.A, MapLane.Top);
        public MapObject LeftTopT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.A, MapLane.Top);
        public MapObject LeftMidT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.A, MapLane.Mid);
        public MapObject LeftMidT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.A, MapLane.Mid);
        public MapObject LeftMidT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.A, MapLane.Mid);
        public MapObject LeftBotT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.A, MapLane.Bot);
        public MapObject LeftBotT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.A, MapLane.Bot);
        public MapObject LeftBotT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.A, MapLane.Bot);

        public MapObject LeftBaseTower { get; set; } = new(MapObjectType.Tier4, MatchSide.A);

        public MapObject LeftAncientNest { get; set; } = new(MapObjectType.AncientNest, MatchSide.A);


        //RIGHT SIDE OF MAP
        public MapObject RightTopT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.B, MapLane.Top);
        public MapObject RightTopT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.B, MapLane.Top);
        public MapObject RightTopT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.B, MapLane.Top);
        public MapObject RightMidT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.B, MapLane.Mid);
        public MapObject RightMidT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.B, MapLane.Mid);
        public MapObject RightMidT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.B, MapLane.Mid);
        public MapObject RightBotT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.B, MapLane.Bot);
        public MapObject RightBotT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.B, MapLane.Bot);
        public MapObject RightBotT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.B, MapLane.Bot);

        public MapObject RightBaseTower { get; set; } = new(MapObjectType.Tier4, MatchSide.B);

        public MapObject RightAncientNest { get; set; } = new(MapObjectType.AncientNest, MatchSide.B);
    }
}
