using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativeLabelElement : NativeVisualElement
    {
        private Text textComponent;
        
        public string Text
        {
            get => textComponent.text;
            set => textComponent.text = value;
        }

        public UnityEngine.Color Foreground
        {
            get => textComponent.color;
            set => textComponent.color = value;
        }

        public int FontSize
        {
            get => textComponent.fontSize;
            set => textComponent.fontSize = value;
        }

        public FontStyle FontStyle
        {
            get => textComponent.fontStyle;
            set => textComponent.fontStyle = value;
        }

        public UnityEngine.Font Font
        {
            get => textComponent.font;
            set => textComponent.font = value;
        }
        
        public HorizontalWrapMode HorizontalWrapMode
        {
            get => textComponent.horizontalOverflow;
            set => textComponent.horizontalOverflow = value;
        }
        
        public override void BuildNativeRenderer()
        {
            base.BuildNativeRenderer();
            
            var self = gameObject;
            self.AddComponent<CanvasRenderer>();
            textComponent = self.AddComponent<Text>();
            
            //Options
            textComponent.text = "";
            textComponent.font = new UnityEngine.Font();
            textComponent.fontStyle = FontStyle.Normal;
            textComponent.fontSize = 14;
            textComponent.lineSpacing = 1;
            textComponent.supportRichText = true;
            textComponent.alignment = TextAnchor.UpperLeft;
            textComponent.alignByGeometry = false;
            textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
            textComponent.verticalOverflow = VerticalWrapMode.Truncate;
            textComponent.resizeTextForBestFit = false;
            textComponent.color = new UnityEngine.Color(0.1960784f,0.1960784f,0.1960784f);
            textComponent.material = null;
            textComponent.raycastTarget = true;
        }
    }
}