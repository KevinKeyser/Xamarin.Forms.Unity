using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Internals;

using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
    public class LabelRenderer : ViewRenderer<Label, NativeLabelElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            UpdateText();
            UpdateColor();
            UpdateAlign();
            UpdateFontSize();
            UpdateFontFamily();
            UpdateFontAttributes();
            UpdateLineBreakMode();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Label.TextProperty.PropertyName ||
                e.PropertyName == Label.FormattedTextProperty.PropertyName)
            {
                UpdateText();
            }
            else if (e.PropertyName == Label.TextColorProperty.PropertyName)
            {
                UpdateColor();
            }
            else if (e.PropertyName == Label.HorizontalTextAlignmentProperty.PropertyName ||
                     e.PropertyName == Label.VerticalTextAlignmentProperty.PropertyName)
            {
                UpdateAlign();
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
            else if (e.PropertyName == Label.LineBreakModeProperty.PropertyName)
            {
                UpdateLineBreakMode();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateText()
        {
            NativeElement.Text = Element.Text;
        }

        private void UpdateColor()
        {
            NativeElement.Foreground = Element.TextColor.ToUnityColor();
        }

        private void UpdateFontSize()
        {
            NativeElement.FontSize = Element.FontSize <= 0
                ? (int)Device.GetNamedSize(NamedSize.Default, Element.GetType())
                : (int)Element.FontSize;
        }

        private void UpdateFontAttributes()
        {
            NativeElement.FontStyle = Element.FontAttributes.ToUnityFontStyle();
        }

        private void UpdateFontFamily()
        {
            NativeElement.Font = FontExtensions.ToUnityFont(Element.FontFamily, NativeElement.FontSize);
        }

        private void UpdateLineBreakMode()
        {
            NativeElement.HorizontalWrapMode = Element.LineBreakMode.ToUnityHorizontalWrapMode();
        }

        private void UpdateAlign()
        {
            NativeElement.HorizontalWrapMode = Element.LineBreakMode.ToUnityHorizontalWrapMode();
        }
    }
}