using System;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativeButtonElement : NativeVisualElement
    {
        private UnityEngine.UI.Button buttonComponent;
        private Text textComponent;

        public EventHandler OnClick;

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

        public override void BuildNativeRenderer()
        {
            base.BuildNativeRenderer();
            var self = gameObject;
            self.AddComponent<CanvasRenderer>();
            buttonComponent = self.AddComponent<UnityEngine.UI.Button>();

            #region Background Image
            var backgroundImage = self.AddComponent<UnityEngine.UI.Image>();
            
            // Options
            backgroundImage.sprite = null;
            backgroundImage.color = UnityEngine.Color.white;
            backgroundImage.material = null;
            backgroundImage.raycastTarget = true;
            backgroundImage.type = UnityEngine.UI.Image.Type.Sliced;
            backgroundImage.fillCenter = true;
            #endregion
            
            #region Text Component
            textComponent = new GameObject($"{self.name} Text",
                                           typeof(RectTransform),
                                           typeof(CanvasRenderer),
                                           typeof(Text))
               .GetComponent<Text>();

            var textRectTransform = textComponent.rectTransform;
            textRectTransform.SetParent(self.transform, false);
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.offsetMin = Vector2.zero;
            textRectTransform.offsetMax = Vector2.zero;

            // Options
            textComponent.text = "Button";
            textComponent.font = new UnityEngine.Font();
            textComponent.fontStyle = FontStyle.Normal;
            textComponent.fontSize = 14;
            textComponent.lineSpacing = 1;
            textComponent.supportRichText = true;
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.alignByGeometry = false;
            textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            textComponent.verticalOverflow = VerticalWrapMode.Truncate;
            textComponent.resizeTextForBestFit = false;
            textComponent.color = new UnityEngine.Color(0.1960784f,0.1960784f,0.1960784f);
            textComponent.material = null;
            textComponent.raycastTarget = true;
            #endregion
            
            #region Button Options
            buttonComponent.interactable = true;
            buttonComponent.transition = Selectable.Transition.ColorTint;
            buttonComponent.targetGraphic = backgroundImage;
            buttonComponent.colors = ColorBlock.defaultColorBlock;
            buttonComponent.navigation = Navigation.defaultNavigation;
            buttonComponent.onClick.AddListener(() => OnClick?.Invoke(this, EventArgs.Empty));
            #endregion
        }
    }
}