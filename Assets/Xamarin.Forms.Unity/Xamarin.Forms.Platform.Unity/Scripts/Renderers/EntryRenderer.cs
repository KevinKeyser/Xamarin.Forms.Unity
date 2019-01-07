using System;
using System.ComponentModel;

using UnityEngine;
using UnityEngine.UI;

using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    public class EntryRenderer : ViewRenderer<Entry, NativeEntryElement>
    {
        public EntryRenderer()
        {
            NativeElement.LineType = UnityEngine.UI.InputField.LineType.SingleLine;
            NativeElement.OnValueChanged += (sender, value) => Element.Text = value;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            UpdateText();
            UpdateTextColor();
            UpdateFontSize();
            UpdateFontFamily();
            UpdateFontAttributes();
            UpdatePlaceholder();
            UpdatePlaceholderColor();
            UpdateInputType();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Entry.TextProperty.PropertyName)
            {
                UpdateText();
            }
            else if (e.PropertyName == Entry.TextColorProperty.PropertyName)
            {
                UpdateTextColor();
            }
            else if (e.PropertyName == Entry.FontSizeProperty.PropertyName || 
                     e.PropertyName == Entry.FontFamilyProperty.PropertyName)
            {
                UpdateFontSize();
                UpdateFontFamily();
            }
            else if (e.PropertyName == Entry.FontAttributesProperty.PropertyName)
            {
                UpdateFontAttributes();
            }
            else if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
            {
                UpdatePlaceholder();
            }
            else if (e.PropertyName == Entry.PlaceholderColorProperty.PropertyName)
            {
                UpdatePlaceholderColor();
            }
            else if (e.PropertyName == InputView.IsSpellCheckEnabledProperty.PropertyName ||
                     e.PropertyName == InputView.KeyboardProperty.PropertyName ||
                     e.PropertyName == Entry.IsPasswordProperty.PropertyName ||
                     e.PropertyName == Entry.IsTextPredictionEnabledProperty.PropertyName)
            {
                UpdateInputType();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateText()
        {
            NativeElement.Text = Element.Text;
        }

        private void UpdateTextColor()
        {
            NativeElement.Foreground = Element.TextColor.ToUnityColor();
        }

        private void UpdateFontSize()
        {
            NativeElement.FontSize = Element.FontSize <= 0 ? (int)Device.GetNamedSize(NamedSize.Default, Element.GetType()) : (int)Element.FontSize;
        }

        private void UpdateFontAttributes()
        {
            NativeElement.FontStyle = Element.FontAttributes.ToUnityFontStyle();
        }

        private void UpdateFontFamily()
        {
            NativeElement.Font = FontExtensions.ToUnityFont(Element.FontFamily, NativeElement.FontSize);
        }

        private void UpdatePlaceholder()
        {
            NativeElement.Placeholder = Element.Placeholder;
        }

        private void UpdatePlaceholderColor()
        {
            NativeElement.PlaceholderColor = Element.PlaceholderColor.ToUnityColor();
        }

        private void UpdateInputType()
        {
            if (Element.IsPassword)
            {
                NativeElement.ContentType = InputField.ContentType.Password;
                NativeElement.InputType = InputField.InputType.Password;

                return;
            }

            NativeElement.InputType = Element.IsSpellCheckEnabled
                ? InputField.InputType.AutoCorrect
                : InputField.InputType.Standard;

            switch (Element.Keyboard)
            {
                case TextKeyboard textKeyboard:
                case ChatKeyboard chatKeyboard:
                    NativeElement.ContentType =
                        Element.IsSpellCheckEnabled
                            ? InputField.ContentType.Autocorrected
                            : InputField.ContentType.Standard;

                    break;
                case EmailKeyboard emailKeyboard:
                    NativeElement.ContentType = InputField.ContentType.EmailAddress;

                    break;
                case UrlKeyboard urlKeyboard:
                case CustomKeyboard customKeyboard:
                    NativeElement.ContentType = InputField.ContentType.Custom;

                    break;
                case NumericKeyboard numericKeyboard:
                    NativeElement.ContentType = InputField.ContentType.DecimalNumber;

                    break;
                case TelephoneKeyboard telephoneKeyboard:
                    NativeElement.ContentType = InputField.ContentType.IntegerNumber;

                    break; 
            }
        }
    }
}