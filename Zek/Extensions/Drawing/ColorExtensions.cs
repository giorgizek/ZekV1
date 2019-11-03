using System;
using System.Drawing;

namespace Zek.Extensions.Drawing
{
    public static class ColorExtensions
    {
        public static Color Interpolate(this Color from, float ratio, Color to)
        {
            var ratioNeg = 1 - ratio;

            return Color.FromArgb(
                (int)(from.A * ratioNeg + to.A * ratio),
                (int)(from.R * ratioNeg + to.R * ratio),
                (int)(from.G * ratioNeg + to.G * ratio),
                (int)(from.B * ratioNeg + to.B * ratio));
        }

        public static string ToHtml(this Color color)
        {
            return ToHtmlColor(color.ToArgb());
        }

        public static string TryToHtml(this Color? color)
        {
            if (color == null)
                return null;

            return ToHtmlColor(color.Value.ToArgb());
        }

        public static string ToHtmlColor(int value)
        {
            return "#" + (value & 0xffffff).ToString("X6");
        }

        public static Color FromHsv(double hue, double saturation, double value)
        {
            var h = hue;
            while (h < 0) { h += 360; }
            while (h >= 360) { h -= 360; }
            double r, g, b;
            if (value <= 0)
            { r = g = b = 0; }
            else if (saturation <= 0)
            {
                r = g = b = value;
            }
            else
            {
                var hf = h / 60.0;
                var i = (int)Math.Floor(hf);
                var f = hf - i;
                var pv = value * (1 - saturation);
                var qv = value * (1 - saturation * f);
                var tv = value * (1 - saturation * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        r = value;
                        g = tv;
                        b = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        r = qv;
                        g = value;
                        b = pv;
                        break;
                    case 2:
                        r = pv;
                        g = value;
                        b = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        r = pv;
                        g = qv;
                        b = value;
                        break;
                    case 4:
                        r = tv;
                        g = pv;
                        b = value;
                        break;

                    // Red is the dominant color

                    case 5:
                        r = value;
                        g = pv;
                        b = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        r = value;
                        g = tv;
                        b = pv;
                        break;
                    case -1:
                        r = value;
                        g = pv;
                        b = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        r = g = b = value; // Just pretend its black/white
                        break;
                }
            }

            return Color.FromArgb(
                Clamp((int)(r * 255.0)),
                Clamp((int)(g * 255.0)),
                Clamp((int)(b * 255.0)));
        }

        static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}
