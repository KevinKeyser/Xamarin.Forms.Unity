using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{

    internal class UnityDeviceInfo : DeviceInfo
    {
        private Size pixelScreenSize = new Size();
        private Size scaledScreenSize = new Size();
        private double scalingFactor = 1.0;

        public override Size PixelScreenSize => pixelScreenSize;

        public override Size ScaledScreenSize => scaledScreenSize;

        public override double ScalingFactor => scalingFactor;

        internal UnityDeviceInfo()
        {
            UpdateProperties();
        }
        
        private void UpdateProperties()
        {
            var current = UnityEngine.Screen.currentResolution;
            var dpi = UnityEngine.Screen.dpi;

            SetPixelScreenSize(new Size(current.width, current.height));
            SetScaledScreenSize(new Size(current.width * dpi, current.height * dpi));
            SetScalingFactor(dpi);
        }
        
        private void SetPixelScreenSize(Size value)
        {
            if (Equals(value, pixelScreenSize))
            {
                return;
            }

            pixelScreenSize = value;
            OnPropertyChanged(nameof(PixelScreenSize));
        }

        private void SetScaledScreenSize(Size value)
        {
            if (Equals(value, scaledScreenSize))
            {
                return;
            }

            scaledScreenSize = value;
            OnPropertyChanged(nameof(ScaledScreenSize));
        }

        private void SetScalingFactor(double value)
        {
            if (Equals(value, scalingFactor))
            {
                return;
            }

            scalingFactor = value;
            OnPropertyChanged(nameof(ScalingFactor));
        }
    }
}