using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Match.GamePerson;

namespace TeamsLeague.BLL.Models.MatchParts.MatchMap
{
    public class MapObject
    {
        public string Name { get; set; } = "MapObject";
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double Health { get; set; }
        public MapObjectType Type { get; set; }
        public MatchSide? Side { get; set; }
        public Location? Location { get; set; }
        public bool IsDestroyed { get; set; } = false;


        public MapObject(MapObjectType type, MatchSide? side = null, Location? line = null)
        {
            Type = type;

            switch (type)
            {
                case MapObjectType.Tier1:
                    Side = side;
                    Location = line;
                    Name = "first " + line + "line tower";
                    Attack = 10;
                    Defense = 0;
                    Health = 100;
                    break;
                case MapObjectType.Tier2:
                    Side = side;
                    Location = line;
                    Name = "second " + line + "line tower";
                    Attack = 15;
                    Defense = 5;
                    Health = 200;
                    break;
                case MapObjectType.Tier3:
                    Side = side;
                    Location = line;
                    Name = "base " + line + " tower";
                    Attack = 20;
                    Defense = 10;
                    Health = 300;
                    break;
                case MapObjectType.Tier4:
                    Side = side;
                    Name = "base Nest tower";
                    Attack = 25;
                    Defense = 15;
                    Health = 400;
                    break;
                case MapObjectType.AncientNest:
                    Side = side;
                    Name = side is MatchSide.A ? "left team Ancient Nest" : "right team Ancient Nest";
                    Attack = 0;
                    Defense = 20;
                    Health = 500;
                    break;
            }
        }
    }
}
