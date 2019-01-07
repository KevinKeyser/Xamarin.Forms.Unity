using System.ComponentModel;

using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
    public class EditorRenderer : ViewRenderer<Editor, NativeEntryElement>
    {
        public EditorRenderer()
        {
            NativeElement.LineType = UnityEngine.UI.InputField.LineType.MultiLineNewline;
            NativeElement.OnValueChanged += (sender, value) => Element.Text = value;
        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
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
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Editor.TextProperty.PropertyName)
            {
                UpdateText();
            }
            else if (e.PropertyName == Editor.TextColorProperty.PropertyName)
            {
                UpdateTextColor();
            }
            else if (e.PropertyName == Editor.FontSizeProperty.PropertyName ||
                     e.PropertyName == Editor.FontFamilyProperty.PropertyName)
            {
                UpdateFontSize();
                UpdateFontFamily();
            }
            else if(e.PropertyName == Editor.FontAttributesProperty.PropertyName)
            {
                UpdateFontAttributes();
            }
            else if (e.PropertyName == Editor.PlaceholderProperty.PropertyName)
            {
                UpdatePlaceholder();
            }
            else if (e.PropertyName == Editor.PlaceholderColorProperty.PropertyName)
            {
                UpdatePlaceholderColor();
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
    }
}