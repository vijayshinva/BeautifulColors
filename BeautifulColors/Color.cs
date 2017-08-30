//MIT License

//Copyright(c) 2017 Vijayshinva Karnure

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;

namespace BeautifulColors
{
    public class Color : IEquatable<Color>, IFormattable
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public Tuple<double, double, double> HSL
        {
            get
            {
                return RgbToHsl(R, G, B);
            }
        }

        public Tuple<double, double, double> HSV
        {
            get
            {
                return RgbToHsv(R, G, B);
            }
        }

        public static Color FromArgb(byte A, byte R, byte G, byte B)
        {
            if (A < 0 || A > 255)
            {
                throw new ArgumentException("Invalid Value", "A");
            }
            if (R < 0 || R > 255)
            {
                throw new ArgumentException("Invalid Value", "R");
            }
            if (G < 0 || G > 255)
            {
                throw new ArgumentException("Invalid Value", "G");
            }
            if (B < 0 || B > 255)
            {
                throw new ArgumentException("Invalid Value", "B");
            }
            return new Color
            {
                A = A,
                R = R,
                G = G,
                B = B
            };
        }
        public static Color FromRgb(byte R, byte G, byte B)
        {
            if (R < 0 || R > 255)
            {
                throw new ArgumentException("Invalid Value", "R");
            }
            if (G < 0 || G > 255)
            {
                throw new ArgumentException("Invalid Value", "G");
            }
            if (B < 0 || B > 255)
            {
                throw new ArgumentException("Invalid Value", "B");
            }
            return new Color
            {
                A = 0xFF,
                R = R,
                G = G,
                B = B
            };
        }

        public static Color FromNamedColor(NamedColors namedColor)
        {
            var namedColorValue = (uint)namedColor;
            var b = namedColorValue & 0xFF;
            var g = (namedColorValue >> 8) & 0xFF;
            var r = (namedColorValue >> 16) & 0xFF;

            return FromRgb((byte)r, (byte)g, (byte)b);
        }

        public static Color FromHSL(double H, double S, double L)
        {
            if (H < 0 || H > 360)
            {
                throw new ArgumentException("Invalid Value", "H");
            }
            if (S < 0 || S > 1)
            {
                throw new ArgumentException("Invalid Value", "S");
            }
            if (L < 0 || L > 1)
            {
                throw new ArgumentException("Invalid Value", "L");
            }
            var rgb = HslToRgb(H, S, L);
            return FromRgb(rgb.Item1, rgb.Item2, rgb.Item3);
        }

        public static Color FromHSV(double H, double S, double V)
        {
            if (H < 0 || H > 360)
            {
                throw new ArgumentException("Invalid Value", "H");
            }
            if (S < 0 || S > 1)
            {
                throw new ArgumentException("Invalid Value", "S");
            }
            if (V < 0 || V > 1)
            {
                throw new ArgumentException("Invalid Value", "V");
            }
            var rgb = HsvToRgb(H, S, V);
            return FromRgb(rgb.Item1, rgb.Item2, rgb.Item3);
        }

        public static Color ColorFromCMYK()
        {
            return new Color();
        }

        private static double HueFromRgb(double r, double g, double b, double max, double chroma)
        {
            double hue = 0;
            if (max == r)
            {
                hue = ((g - b) / chroma) % 6;
            }
            else if (max == g)
            {
                hue = ((b - r) / chroma) + 2;
            }
            else if (max == b)
            {
                hue = ((r - g) / chroma) + 4;
            }
            hue = 60 * hue;
            return hue;
        }

        private static Tuple<byte, byte, byte> HsvToRgb(double H, double S, double V)
        {
            var chroma = V * S;
            var rgb = HueChromaToRGB(H, chroma);

            var m = V - chroma;

            var r = rgb.Item1 + m;
            var g = rgb.Item2 + m;
            var b = rgb.Item3 + m;
            return new Tuple<byte, byte, byte>((byte)Math.Round(r * 255), (byte)Math.Round(g * 255), (byte)Math.Round(b * 255));
        }

        private static Tuple<double, double, double> HueChromaToRGB(double hue, double chroma)
        {
            double r, g, b;
            r = g = b = 0;
            var h = hue / 60;
            var x = chroma * (1 - Math.Abs((h % 2) - 1));
            if (0 <= h && h <= 1)
            {
                r = chroma;
                g = x;
                b = 0;
            }
            else if (1 <= h && h <= 2)
            {
                r = x;
                g = chroma;
                b = 0;
            }
            else if (2 <= h && h <= 3)
            {
                r = 0;
                g = chroma;
                b = x;
            }
            else if (3 <= h && h <= 4)
            {
                r = 0;
                g = x;
                b = chroma;
            }
            else if (4 <= h && h <= 5)
            {
                r = x;
                g = 0;
                b = chroma;
            }
            else if (5 <= h && h <= 6)
            {
                r = chroma;
                g = 0;
                b = x;
            }

            return new Tuple<double, double, double>(r, g, b);
        }

        private static Tuple<byte, byte, byte> HslToRgb(double H, double S, double L)
        {
            var chroma = (1 - Math.Abs((2 * L) - 1)) * S;

            var rgb = HueChromaToRGB(H, chroma);

            var m = L - (chroma / 2);

            var r = rgb.Item1 + m;
            var g = rgb.Item2 + m;
            var b = rgb.Item3 + m;
            return new Tuple<byte, byte, byte>((byte)Math.Round(r * 255), (byte)Math.Round(g * 255), (byte)Math.Round(b * 255));
        }

        private Tuple<double, double, double> RgbToHsl(byte R, byte G, byte B)
        {
            var r = (double)R / 255;
            var g = (double)G / 255;
            var b = (double)B / 255;

            var max = Math.Max(Math.Max(r, g), b);
            var min = Math.Min(Math.Min(r, g), b);
            var chroma = max - min;
            var hue = HueFromRgb(r, g, b, max, chroma);

            var luminance = (max + min) / 2;

            double saturation = 0;
            if (luminance != 1)
            {
                saturation = chroma / (1 - Math.Abs((2 * luminance) - 1));
            }

            return new Tuple<double, double, double>(hue, saturation, luminance);
        }

        private Tuple<double, double, double> RgbToHsv(byte R, byte G, byte B)
        {
            var r = (double)R / 255;
            var g = (double)G / 255;
            var b = (double)B / 255;

            var max = Math.Max(Math.Max(r, g), b);
            var min = Math.Min(Math.Min(r, g), b);
            var chroma = max - min;
            var hue = HueFromRgb(r, g, b, max, chroma);

            var value = max;

            double saturation = 0;
            if (value != 0)
            {
                saturation = chroma / value;
            }

            return new Tuple<double, double, double>(hue, saturation, value);
        }

        private Tuple<double, double, double> RgbToHsi(byte R, byte G, byte B)
        {
            var r = (double)R / 255;
            var g = (double)G / 255;
            var b = (double)B / 255;

            var max = Math.Max(Math.Max(r, g), b);
            var min = Math.Min(Math.Min(r, g), b);
            var chroma = max - min;
            var hue = HueFromRgb(r, g, b, max, chroma);

            var intensity = (r + g + b) / 3;

            double saturation = 0;
            if (intensity != 0)
            {
                saturation = 1 - (min / intensity);
            }

            return new Tuple<double, double, double>(hue, saturation, intensity);
        }

        public bool Equals(Color other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                Color other = (Color)obj;
                return (this == other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1960784236;
            hashCode = (hashCode * -1521134295) + base.GetHashCode();
            hashCode = (hashCode * -1521134295) + R.GetHashCode();
            hashCode = (hashCode * -1521134295) + G.GetHashCode();
            hashCode = (hashCode * -1521134295) + B.GetHashCode();
            hashCode = (hashCode * -1521134295) + A.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Color color1, Color color2)
        {
            if (color1.R != color2.R)
            {
                return false;
            }
            if (color1.G != color2.G)
            {
                return false;
            }
            if (color1.B != color2.B)
            {
                return false;
            }
            if (color1.A != color2.A)
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(Color color1, Color color2)
        {
            return !(color1 == color2);
        }

        public override string ToString()
        {
            return BitConverter.ToString(new byte[] { R, G, B });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "RGB":
                    return $"{R},{G},{B}";
                case "RGBA":
                    return $"{R},{G},{B},{A}";
                case "HSL":
                    var hsl = RgbToHsl(R, G, B);
                    return $"{hsl.Item1},{hsl.Item2},{hsl.Item3}";
                case "HSV":
                    var hsv = RgbToHsv(R, G, B);
                    return $"{hsv.Item1},{hsv.Item2},{hsv.Item3}";
                case "HEX":
                    return "#" + BitConverter.ToString(new byte[] { R, G, B }).Replace("-", "");
                default:
                    return BitConverter.ToString(new byte[] { R, G, B });
            }
        }
    }
}
