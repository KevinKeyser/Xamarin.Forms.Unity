using System;

using UnityEngine;

using XamarinFont = Xamarin.Forms.Font;
using UnityFont = UnityEngine.Font;

namespace Xamarin.Forms.Platform.Unity
{
    public static class FontExtensions
    {
        public static UnityFont ToUnityFont(this XamarinFont font, out int fontSize, out FontStyle fontStyle)
        {
            fontSize = font.UseNamedSize
                ? (int)Device.GetNamedSize(NamedSize.Default, typeof(object))
                : (int)font.FontSize;

            fontStyle = font.FontAttributes.ToUnityFontStyle();

            return ToUnityFont(font.FontFamily, fontSize);
        }

        public static UnityFont ToUnityFont(string fontFamily, int fontSize)
        {
            return UnityFont.CreateDynamicFontFromOSFont(String.IsNullOrWhiteSpace(fontFamily) ? "Arial" : fontFamily, fontSize);
        }

        public static FontStyle ToUnityFontStyle(this FontAttributes fontAttributes)
        {
            var bold = fontAttributes.HasFlag(FontAttributes.Bold);
            var italic = fontAttributes.HasFlag(FontAttributes.Italic);

            return bold && italic
                ? FontStyle.BoldAndItalic
                : bold
                    ? FontStyle.Bold
                    : italic
                        ? FontStyle.Italic
                        : FontStyle.Normal;
        }

        public static FontAttributes ToXamarinFontAttributes(this FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case FontStyle.Normal:

                    return FontAttributes.None;

                case FontStyle.Bold:

                    return FontAttributes.Bold;

                case FontStyle.Italic:

                    return FontAttributes.Italic;

                case FontStyle.BoldAndItalic:

                    return FontAttributes.Bold & FontAttributes.Italic;

                default:

                    throw new ArgumentOutOfRangeException(nameof(fontStyle), fontStyle, null);
            }
        }
    }
}