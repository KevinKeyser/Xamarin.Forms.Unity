using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativeImageElement : NativeVisualElement
    {
        private RawImage imageComponent;

        public Texture Texture
        {
            get => imageComponent.texture;
            set => imageComponent.texture = value;
        }
        
        public override void BuildNativeRenderer()
        {
            base.BuildNativeRenderer();
            
            var self = gameObject;
            self.AddComponent<CanvasRenderer>();
            imageComponent = self.AddComponent<RawImage>();
            
            // Options
            imageComponent.texture = null;
            imageComponent.color = UnityEngine.Color.white;
            imageComponent.material = null;
            imageComponent.raycastTarget = true;
            imageComponent.uvRect = new Rect(0, 0, 1, 1);
        }
    }
}