using System;
using System.Collections.Generic;

using UnityEngine;

using System.ComponentModel;

using UnityEngine.EventSystems;

using System.Collections.Specialized;

using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class PickerRenderer : ViewRenderer<Picker, NativePickerElement>
    {
        public PickerRenderer()
        {
            NativeElement.OnSelectionChanged += (sender, value) => Element.SelectedIndex = value;
        }

        #region Event Handler
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            e.OldElement?.Items.RemoveCollectionChangedEvent(OnCollectionChanged);

            if (e.NewElement == null)
            {
                return;
            }

            e.NewElement.Items.AddCollectionChangedEvent(OnCollectionChanged);

            NativeElement.Options = CreateOptionData(e.NewElement.Items);

            UpdateSelectedIndex();
            UpdateTextColor();
            UpdatePicker();
            UpdateFontSize();
            UpdateFontFamily();
            UpdateFontAttributes();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
            {
                UpdateSelectedIndex();
            }
            else if (e.PropertyName == Picker.TextColorProperty.PropertyName)
            {
                UpdateTextColor();
            }
            else if (e.PropertyName == Picker.ItemsSourceProperty.PropertyName)
            {
                UpdatePicker();
            }
            else if (e.PropertyName == Picker.FontSizeProperty.PropertyName || 
                     e.PropertyName == Picker.FontFamilyProperty.PropertyName)
            {
                UpdateFontSize();
                UpdateFontFamily();
            }
            else if (e.PropertyName == Picker.FontAttributesProperty.PropertyName)
            {
                UpdateFontAttributes();
            }

            base.OnElementPropertyChanged(sender, e);
        }
        #endregion

        private void UpdateSelectedIndex()
        {
            NativeElement.SelectedIndex = Element.SelectedIndex;
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

        private void UpdatePicker()
        {
            var index = Element.SelectedIndex;

            if (index < 0 || index >= Element.Items.Count)
            {
                NativeElement.CaptionText = String.Empty;
            }
            else
            {
                NativeElement.CaptionText = Element.Items[index];
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NativeElement.Options = CreateOptionData(Element.Items);
            UpdatePicker();
        }

        private static List<UnityEngine.UI.Dropdown.OptionData> CreateOptionData(IList<string> source)
        {
            if (source == null)
            {
                return null;
            }

            var count = source.Count;
            var options = new List<UnityEngine.UI.Dropdown.OptionData>(count);

            for (var i = 0; i < count; i++)
            {
                options.Add(new UnityEngine.UI.Dropdown.OptionData(source[i]));
            }

            return options;
        }
    }
}