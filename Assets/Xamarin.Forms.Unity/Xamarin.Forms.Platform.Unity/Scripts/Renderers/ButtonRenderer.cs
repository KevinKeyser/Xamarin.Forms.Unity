using System;
using System.ComponentModel;

using UnityEngine;

using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    public class ButtonRenderer : ViewRenderer<Button, NativeButtonElement>
    {
        public ButtonRenderer()
        {
            NativeElement.OnClick += (sender, args) =>
            {
                (Element as IButtonController)?.SendClicked();
            };
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
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
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Button.TextProperty.PropertyName)
            {
                UpdateText();
            }
            else if (e.PropertyName == Button.TextColorProperty.PropertyName)
            {
                UpdateTextColor();
            }
            else if (e.PropertyName == Button.FontSizeProperty.PropertyName ||
                     e.PropertyName == Button.FontFamilyProperty.PropertyName)
            {
                UpdateFontSize();
                UpdateFontFamily();
            }
            else if(e.PropertyName == Button.FontAttributesProperty.PropertyName)
            {
                UpdateFontAttributes();
            }
            else if (e.PropertyName == Button.FontProperty.PropertyName)
            {
                UpdateFontSize();
                UpdateFontFamily();
                UpdateFontAttributes();
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
            NativeElement.Font = Element.Font.ToUnityFont(out var _, out var _);
        }

    }
}