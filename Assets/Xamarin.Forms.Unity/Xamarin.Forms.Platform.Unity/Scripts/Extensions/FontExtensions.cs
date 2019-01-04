using System;

using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
    public static class FontExtensions
    {
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