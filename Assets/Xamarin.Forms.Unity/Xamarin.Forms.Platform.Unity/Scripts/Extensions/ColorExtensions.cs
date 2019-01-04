using UnityColor = UnityEngine.Color;
using XamarinColor = Xamarin.Forms.Color;

namespace Xamarin.Forms.Platform.Unity
{
    public static class ColorExtensions
    {
        public static UnityColor ToUnityColor(this XamarinColor color)
        {
            return color == XamarinColor.Default
                ? UnityColor.black // Default Temporary
                : new UnityColor((float)color.R, (float)color.G, (float)color.B, (float)color.A);
        }

        public static XamarinColor ToXamarinColor(this UnityColor color)
        {
            return color == UnityColor.black // Default Check
                ? XamarinColor.Default
                : new XamarinColor(color.r, color.g, color.b, color.a);
        }
    }
}