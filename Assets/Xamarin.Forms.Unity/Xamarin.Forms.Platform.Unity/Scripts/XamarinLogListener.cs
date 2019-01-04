using UnityEngine;

using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    internal class XamarinLogListener : LogListener
    {
        public override void Warning(string category, string message)
        {
            Debug.LogWarning($"[{category}] {message}");
        }
    }
}
