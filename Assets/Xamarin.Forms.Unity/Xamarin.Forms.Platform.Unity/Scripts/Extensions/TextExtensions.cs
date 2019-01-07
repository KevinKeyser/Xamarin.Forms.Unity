using UnityEngine;

using Xamarin.Forms;


using UnityTextAlignment = UnityEngine.TextAlignment;
using XamarinTextAlignment = Xamarin.Forms.TextAlignment;

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
        
        public static TextAnchor ToUnityTextAnchor(XamarinTextAlignment horizonatal, XamarinTextAlignment vertical)
        {
            switch (horizonatal)
            {
                case XamarinTextAlignment.Start:
                    switch (vertical)
                    {
                        case XamarinTextAlignment.Start:  return TextAnchor.UpperLeft;
                        case XamarinTextAlignment.Center: return TextAnchor.MiddleLeft;
                        case XamarinTextAlignment.End:    return TextAnchor.LowerLeft;
                        default:

                            throw new ArgumentOutOfRangeException(nameof(vertical), vertical, null);
                    }
                    break;

                case XamarinTextAlignment.Center:
                    switch (vertical)
                    {
                        case XamarinTextAlignment.Start:  return TextAnchor.UpperCenter;
                        case XamarinTextAlignment.Center: return TextAnchor.MiddleCenter;
                        case XamarinTextAlignment.End:    return TextAnchor.LowerCenter;
                        default:

                            throw new ArgumentOutOfRangeException(nameof(vertical), vertical, null);
                    }
                    break;

                case XamarinTextAlignment.End:
                    switch (vertical)
                    {
                        case XamarinTextAlignment.Start:  return TextAnchor.UpperRight;
                        case XamarinTextAlignment.Center: return TextAnchor.MiddleRight;
                        case XamarinTextAlignment.End:    return TextAnchor.LowerRight;
                        default:

                            throw new ArgumentOutOfRangeException(nameof(vertical), vertical, null);
                    }
                    break;
                default:

                    throw new ArgumentOutOfRangeException(nameof(horizonatal), horizonatal, null);
            }
            return TextAnchor.MiddleCenter;
        }
    }
}