using System;
using SquidCraft.Extensions;

namespace SquidCraft.Text
{
    public static class TextComponentHelper
    {
        public static readonly TextStyles[] Styles =
        {
            TextStyles.Bold, TextStyles.Italic, TextStyles.Underlined, TextStyles.Strikethrough, TextStyles.Obfuscated
        };

        private static readonly string[] ColorNames;
        private static readonly string[] StyleNames;

        static TextComponentHelper()
        {
            var colorNames = Enum.GetNames(typeof(TextColor));
            ColorNames = new string[colorNames.Length];
            for (var i = 0; i < colorNames.Length; i++)
                ColorNames[i] = colorNames[i].ToUnderscoreCase();

            StyleNames = new string[StyleNames.Length];
            for (var i = 0; i < StyleNames.Length; i++)
            {
                var style = Styles[i];
                var styleName = Enum.GetName(typeof(TextStyles), style);
                StyleNames[i] = styleName?.ToUnderscoreCase() ??
                                throw new NullReferenceException("No name for style " + style);
            }
        }

        public static string GetColorName(this TextColor color)
        {
            return ColorNames[(byte) color];
        }

        public static TextColor GetColorFromName(string name)
        {
            var index = Array.IndexOf(ColorNames, name);
            if (index == -1)
                throw new ArgumentOutOfRangeException(nameof(name), "Invalid color name");
            return (TextColor) index;
        }

        public static string GetStyleName(this TextStyles styles)
        {
            return StyleNames[(int) styles];
        }
    }
}