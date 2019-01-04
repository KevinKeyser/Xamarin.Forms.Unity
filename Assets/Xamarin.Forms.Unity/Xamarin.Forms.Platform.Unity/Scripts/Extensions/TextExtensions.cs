using UnityEngine;

using Xamarin.Forms;

namespace System.Collections.Generic
{
    public static class TextExtensions
    {
        public static HorizontalWrapMode ToUnityHorizontalWrapMode(this LineBreakMode mode)
        {
            return mode == LineBreakMode.CharacterWrap
                   || mode == LineBreakMode.WordWrap
                ? HorizontalWrapMode.Wrap
                : HorizontalWrapMode.Overflow;
        }

        public static VerticalWrapMode ToUnityVerticalWrapMode(this LineBreakMode mode)
        {
            return mode == LineBreakMode.HeadTruncation
                   || mode == LineBreakMode.MiddleTruncation
                   || mode == LineBreakMode.TailTruncation
                ? VerticalWrapMode.Truncate
                : VerticalWrapMode.Overflow;
        }
    }
}