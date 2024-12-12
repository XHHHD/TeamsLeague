using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Match.GamePerson;

namespace TeamsLeague.BLL.Models.MatchParts.MatchMap
{
    public class MatchMapVM
    {
        //LEFT SIDE OF MAP
        public MapObject LeftTopT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.A, Location.TopLine);
        public MapObject LeftTopT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.A, Location.TopLine);
        public MapObject LeftTopT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.A, Location.TopLine);
        public MapObject LeftMidT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.A, Location.MidLine);
        public MapObject LeftMidT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.A, Location.MidLine);
        public MapObject LeftMidT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.A, Location.MidLine);
        public MapObject LeftBotT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.A, Location.BotLine);
        public MapObject LeftBotT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.A, Location.BotLine);
        public MapObject LeftBotT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.A, Location.BotLine);

        public MapObject LeftBaseTower { get; set; } = new(MapObjectType.Tier4, MatchSide.A);

        public MapObject LeftAncientNest { get; set; } = new(MapObjectType.AncientNest, MatchSide.A);


        //RIGHT SIDE OF MAP
        public MapObject RightTopT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.B, Location.TopLine);
        public MapObject RightTopT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.B, Location.TopLine);
        public MapObject RightTopT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.B, Location.TopLine);
        public MapObject RightMidT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.B, Location.MidLine);
        public MapObject RightMidT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.B, Location.MidLine);
        public MapObject RightMidT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.B, Location.MidLine);
        public MapObject RightBotT1 { get; set; } = new(MapObjectType.Tier1, MatchSide.B, Location.BotLine);
        public MapObject RightBotT2 { get; set; } = new(MapObjectType.Tier2, MatchSide.B, Location.BotLine);
        public MapObject RightBotT3 { get; set; } = new(MapObjectType.Tier3, MatchSide.B, Location.BotLine);

        public MapObject RightBaseTower { get; set; } = new(MapObjectType.Tier4, MatchSide.B);

        public MapObject RightAncientNest { get; set; } = new(MapObjectType.AncientNest, MatchSide.B);

        public HashSet<MapObject> AllObjects { get; set; }


        public bool IsLateGameIsStarted { get; set; }


        public MatchMapVM()
        {
            AllObjects = new HashSet<MapObject>
            {
                LeftTopT1,
                LeftTopT2,
                LeftTopT3,
                LeftMidT1,
                LeftMidT2,
                LeftMidT3,
                LeftBotT1,
                LeftBotT2,
                LeftBotT3,
                LeftBaseTower,
                LeftAncientNest,

                RightTopT1,
                RightTopT2,
                RightTopT3,
                RightMidT1,
                RightMidT2,
                RightMidT3,
                RightBotT1,
                RightBotT2,
                RightBotT3,
                RightBaseTower,
                RightAncientNest,
            };

            IsLateGameIsStarted = false;
        }
    }
}
