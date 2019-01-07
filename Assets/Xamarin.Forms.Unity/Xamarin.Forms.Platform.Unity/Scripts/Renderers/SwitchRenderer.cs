using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using System.ComponentModel;

namespace Xamarin.Forms.Platform.Unity
{
    public class SwitchRenderer : ViewRenderer<Switch, NativeSwitchElement>
    {
        public SwitchRenderer()
        {
            NativeElement.OnToggled += (sender, args) =>
            {
                Element.IsToggled = args;
            };
        }

        #region Event Handler
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }
            
            UpdateToggle();
            UpdateOnColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Switch.IsToggledProperty.PropertyName)
            {
                UpdateToggle();
            }
            else if (e.PropertyName == Switch.OnColorProperty.PropertyName)
            {
                UpdateOnColor();
            }

            base.OnElementPropertyChanged(sender, e);
        }
        #endregion

        private void UpdateToggle()
        {
            NativeElement.IsToggled = Element.IsToggled;
        }

        private void UpdateOnColor()
        {
            NativeElement.OnColor = Element.OnColor.ToUnityColor();
        }
    }
}