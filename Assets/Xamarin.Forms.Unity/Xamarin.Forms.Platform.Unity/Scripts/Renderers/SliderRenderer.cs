using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using System.ComponentModel;

namespace Xamarin.Forms.Platform.Unity
{
    public class SliderRenderer : ViewRenderer<Slider, NativeSliderElement>
    {
        public SliderRenderer()
        {
            NativeElement.OnValueChanged += (sender, args) => Element.Value = args;

            NativeElement.OnMinimumChanged += (sender, args) => Element.Minimum = args;
            NativeElement.OnMaximumChanged += (sender, args) => Element.Maximum = args;
        }
       
        #region Event Handler
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            UpdateValue();
            UpdateMinimum();
            UpdateMaximum();
            UpdateHandleColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Slider.ValueProperty.PropertyName)
            {
                UpdateValue();
            }
            else if (e.PropertyName == Slider.MinimumProperty.PropertyName)
            {
                UpdateMinimum();
            }
            else if (e.PropertyName == Slider.MaximumProperty.PropertyName)
            {
                UpdateMaximum();
            }
            else if (e.PropertyName == Slider.ThumbColorProperty.PropertyName)
            {
                UpdateHandleColor();
            }
            
            base.OnElementPropertyChanged(sender, e);
        }
        #endregion

        private void UpdateValue()
        {
            NativeElement.Value = (float)Element.Value;
        }

        private void UpdateMinimum()
        {
            NativeElement.Minimum = (float)Element.Minimum;
        }

        private void UpdateMaximum()
        {
            NativeElement.Maximum = (float)Element.Maximum;
        }

        private void UpdateHandleColor()
        {
            NativeElement.HandleColor = Element.ThumbColor.ToUnityColor();
        }
    }
}