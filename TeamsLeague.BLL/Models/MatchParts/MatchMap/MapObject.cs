using TeamsLeague.DAL.Constants.Match;

namespace TeamsLeague.BLL.Models.MatchParts.MatchMap
{
    public class MapObject
    {
        public string Name { get; set; } = "MapObject";
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public MapObjectType Type { get; set; }
        public MatchSide? Side { get; set; }
        public MapLane? Line { get; set; }
        public bool isDestroyed { get; set; } = false;


        public MapObject(MapObjectType type, MatchSide? side = null, MapLane? line = null)
        {
            Type = type;

            switch (type)
            {
                case MapObjectType.Tier1:
                    Side = side;
                    Line = line;
                    Name = "first " + line + "line tower";
                    Attack = 10;
                    Defense = 10;
                    Health = 100;
                    break;
                case MapObjectType.Tier2:
                    Side = side;
                    Line = line;
                    Name = "second " + line + "line tower";
                    Attack = 20;
                    Defense = 20;
                    Health = 200;
                    break;
                case MapObjectType.Tier3:
                    Side = side;
                    Line = line;
                    Name = "base " + line + " tower";
                    Attack = 30;
                    Defense = 30;
                    Health = 300;
                    break;
                case MapObjectType.Tier4:
                    Side = side;
                    Name = "base Nest tower";
                    Attack = 40;
                    Defense = 40;
                    Health = 400;
                    break;
                case MapObjectType.AncientNest:
                    Side = side;
                    Name = side is MatchSide.A ? "left team Ancient Nest" : "right team Ancient Nest";
                    Attack = 0;
                    Defense = 50;
                    Health = 500;
                    break;
            }
        }
    }
}
