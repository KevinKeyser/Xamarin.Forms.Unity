using System;
using System.Xml;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class NativeEntryElement : NativeVisualElement
    {
        protected InputField inputFieldComponent;
        protected Text placeholderTextComponent;
        protected Text textComponent;

        public EventHandler<string> OnValueChanged;

        public InputField.LineType LineType
        {
            get => inputFieldComponent.lineType;
            set => inputFieldComponent.lineType = value;
        }

        public InputField.InputType InputType
        {
            get => inputFieldComponent.inputType;
            set => inputFieldComponent.inputType = value;
        }

        public InputField.ContentType ContentType
        {
            get => inputFieldComponent.contentType;
            set => inputFieldComponent.contentType = value;
        }
        
        public string Text
        {
            get => inputFieldComponent.text;
            set => inputFieldComponent.text = value;
        }

        public string Placeholder
        {
            get => placeholderTextComponent.text;
            set => placeholderTextComponent.text = value;
        }

        public UnityEngine.Color Foreground
        {
            get => textComponent.color;
            set => textComponent.color = value;
        }
        
        public UnityEngine.Color PlaceholderColor
        {
            get => placeholderTextComponent.color;
            set => placeholderTextComponent.color = value;
        }

        public int FontSize
        {
            get => textComponent.fontSize;
            set
            {
                textComponent.fontSize = value;
                placeholderTextComponent.fontSize = value;
            }
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
            
            inputFieldComponent = self.AddComponent<InputField>();
            #region Background Image
            var backgroundImage = self.AddComponent<UnityEngine.UI.Image>();
            backgroundImage.sprite = null;
            backgroundImage.color = UnityEngine.Color.white;
            backgroundImage.material = null;
            backgroundImage.raycastTarget = true;
            backgroundImage.type = UnityEngine.UI.Image.Type.Sliced;
            backgroundImage.fillCenter = true;
            #endregion
            
            #region Placeholder
            placeholderTextComponent = new GameObject($"{self.name} Placeholder",
                                                      typeof(RectTransform),
                                                      typeof(CanvasRenderer),
                                                      typeof(Text))
               .GetComponent<Text>();

            var placeholderRectTransform = placeholderTextComponent.rectTransform;

            placeholderRectTransform.SetParent(self.transform, false);
            placeholderRectTransform.anchorMin = Vector2.zero;
            placeholderRectTransform.anchorMax = Vector2.one;
            placeholderRectTransform.offsetMin = new Vector2(10, 6);
            placeholderRectTransform.offsetMax = -new Vector2(10, 7);
            
            //Options
            placeholderTextComponent.text = "Enter text...";
            placeholderTextComponent.fontStyle = FontStyle.Italic;
            placeholderTextComponent.fontSize = 14;
            placeholderTextComponent.font = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", placeholderTextComponent.fontSize);
            placeholderTextComponent.lineSpacing = 1;
            placeholderTextComponent.supportRichText = true;
            placeholderTextComponent.alignment = TextAnchor.UpperLeft;
            placeholderTextComponent.alignByGeometry = false;
            placeholderTextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            placeholderTextComponent.verticalOverflow = VerticalWrapMode.Truncate;
            placeholderTextComponent.resizeTextForBestFit = false;
            placeholderTextComponent.color = new UnityEngine.Color(0.1960784f,0.1960784f,0.1960784f, 0.5019608f);
            placeholderTextComponent.material = null;
            placeholderTextComponent.raycastTarget = true;
            #endregion

            #region Text
            textComponent = new GameObject($"{gameObject.name} Text",
                                           typeof(RectTransform),
                                           typeof(CanvasRenderer),
                                           typeof(Text))
               .GetComponent<Text>();

            var textRectTransform = textComponent.rectTransform;

            textRectTransform.SetParent(self.transform);
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.offsetMin = new Vector2(10, 6);
            textRectTransform.offsetMax = -new Vector2(10, 7);

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
            #endregion
            
            #region InputField Options
            inputFieldComponent.interactable = true;
            // Transitions
            inputFieldComponent.transition = Selectable.Transition.ColorTint;
            inputFieldComponent.targetGraphic = backgroundImage;
            inputFieldComponent.colors = ColorBlock.defaultColorBlock;
            // Navigation
            inputFieldComponent.navigation = Navigation.defaultNavigation;
            // Text Options
            inputFieldComponent.textComponent = textComponent;
            inputFieldComponent.text = "";
            inputFieldComponent.characterLimit = 0;
            inputFieldComponent.inputType = InputField.InputType.Standard;
            inputFieldComponent.contentType = InputField.ContentType.Standard;
            inputFieldComponent.lineType = InputField.LineType.SingleLine;
            inputFieldComponent.placeholder = placeholderTextComponent;
            inputFieldComponent.caretBlinkRate = .85f;
            inputFieldComponent.caretWidth = 1;
            inputFieldComponent.customCaretColor = false;
            inputFieldComponent.selectionColor = new UnityEngine.Color(.6588235f, 0.8078431f, 1f, 0.7529412f);
            inputFieldComponent.shouldHideMobileInput = false;
            inputFieldComponent.readOnly = false;

            // inputFieldComponent.onValidateInput
            inputFieldComponent.onValueChanged.AddListener(value => OnValueChanged?.Invoke(this, value));
            // inputFieldComponent.onEndEdit
            #endregion
        }
    }
}