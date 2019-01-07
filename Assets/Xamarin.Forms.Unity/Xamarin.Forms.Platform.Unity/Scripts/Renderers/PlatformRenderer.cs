using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class PlatformRenderer
    {
        public Canvas Canvas { get; }
        public RectTransform RectTransform { get; }

        public PlatformRenderer()
        {
            var gameObject = new GameObject(nameof(PlatformRenderer));
            
            Canvas = gameObject.AddComponent<Canvas>();
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            RectTransform = gameObject.GetComponent<RectTransform>();
            RectTransform.pivot = Vector2.zero;
            RectTransform.anchorMin = Vector2.zero;
            RectTransform.anchorMax = Vector2.one;
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;

            gameObject.AddComponent<GraphicRaycaster>();
        }
    }
}
