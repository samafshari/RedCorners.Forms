using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace RedCorners.Forms
{
    public static class Palette
    {
        public static Color Color0 => Colors[0];
        public static Color Color1 => Colors[1];
        public static Color Color2 => Colors[2];
        public static Color Color3 => Colors[3];
        public static Color Color4 => Colors[4];
        public static Color Color5 => Colors[5];
        public static Color Color6 => Colors[6];
        public static Color Color7 => Colors[7];
        public static Color Color8 => Colors[8];
        public static Color Color9 => Colors[9];
        public static Color Color10 => Colors[10];
        public static Color Color11 => Colors[11];
        public static Color Color12 => Colors[12];
        public static Color Color13 => Colors[13];
        public static Color Color14 => Colors[14];
        public static Color Color15 => Colors[15];
        public static Color Color16 => Colors[16];
        public static Color Color17 => Colors[17];
        public static Color Color18 => Colors[18];
        public static Color Color19 => Colors[19];

        public static readonly Color[] Colors = new[]
        {
            Color.FromHex("#c62828"),
            Color.FromHex("#6A1B9A"),
            Color.FromHex("#283593"),
            Color.FromHex("#0277BD"),
            Color.FromHex("#00695C"),
            Color.FromHex("#558B2F"),
            Color.FromHex("#F9A825"),
            Color.FromHex("#EF6C00"),
            Color.FromHex("#4E342E"),
            Color.FromHex("#37474F"),
            Color.FromHex("#AD1457"),
            Color.FromHex("#4527A0"),
            Color.FromHex("#1565C0"),
            Color.FromHex("#00838F"),
            Color.FromHex("#2E7D32"),
            Color.FromHex("#9E9D24"),
            Color.FromHex("#FF8F00"),
            Color.FromHex("#D84315"),
            Color.FromHex("#424242"),

            Color.FromHex("#b71c1c"),
            Color.FromHex("#4A148C"),
            Color.FromHex("#1A237E"),
            Color.FromHex("#01579B"),
            Color.FromHex("#004D40"),
            Color.FromHex("#33691E"),
            Color.FromHex("#F57F17"),
            Color.FromHex("#E65100"),
            Color.FromHex("#3E2723"),

            Color.FromHex("#263238"),
            Color.FromHex("#880E4F"),
            Color.FromHex("#311B92"),
            Color.FromHex("#0D47A1"),
            Color.FromHex("#006064"),
            Color.FromHex("#1B5E20"),
            Color.FromHex("#827717"),
            Color.FromHex("#FF6F00"),
            Color.FromHex("#BF360C"),
            Color.FromHex("#212121"),
        };

        public static readonly Color[] BrightColors = new[]
        {
            Color.FromHex("#ff1744"),
            Color.FromHex("#D500F9"),
            Color.FromHex("#3D5AFE"),
            Color.FromHex("#00B0FF"),
            Color.FromHex("#1DE9B6"),
            Color.FromHex("#76FF03"),
            Color.FromHex("#FFEA00"),
            Color.FromHex("#FF9100"),
            Color.FromHex("#F50057"),
            Color.FromHex("#651FFF"),
            Color.FromHex("#2979FF"),
            Color.FromHex("#00E5FF"),
            Color.FromHex("#00E676"),
            Color.FromHex("#C6FF00"),
            Color.FromHex("#FFC400"),
            Color.FromHex("#FF3D00")
        };

        public static readonly List<Color> AllColors = Colors.Union(BrightColors).ToList();

        public static Color BrightColor0 => BrightColors[0];
        public static Color BrightColor1 => BrightColors[1];
        public static Color BrightColor2 => BrightColors[2];
        public static Color BrightColor3 => BrightColors[3];
        public static Color BrightColor4 => BrightColors[4];
        public static Color BrightColor5 => BrightColors[5];
        public static Color BrightColor6 => BrightColors[6];
        public static Color BrightColor7 => BrightColors[7];
        public static Color BrightColor8 => BrightColors[8];
        public static Color BrightColor9 => BrightColors[9];
        public static Color BrightColor10 => BrightColors[10];
        public static Color BrightColor11 => BrightColors[11];
        public static Color BrightColor12 => BrightColors[12];
        public static Color BrightColor13 => BrightColors[13];
        public static Color BrightColor14 => BrightColors[14];
        public static Color BrightColor15 => BrightColors[15];
        public static Color BrightColor16 => BrightColors[16];
        public static Color BrightColor17 => BrightColors[17];
        public static Color BrightColor18 => BrightColors[18];
        public static Color BrightColor19 => BrightColors[19];
    }
}
