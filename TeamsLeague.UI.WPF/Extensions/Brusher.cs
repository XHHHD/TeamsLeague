using System.Windows.Media;

namespace TeamsLeague.UI.WPF.Extensions
{
    public static class Brusher
    {
        public static Color GetColorByLevel(double level) => new()
        {
            A = 255,

            R = (byte)(level >= 254 && level <= 381
            ? (level - 254) * 2
            : level > 444
            ? 255
            : 0),

            G = (byte)(level >= 127 && level <= 254
            ? (level - 127) * 2
            : level < 317 && level > 127
            ? 381 - (level - 254) * 4
            : 0),

            B = (byte)(level <= 127
            ? level * 2
            : level < 191
            ? 255 - (level - 127) * 4
            : 0),
        };
    }
}