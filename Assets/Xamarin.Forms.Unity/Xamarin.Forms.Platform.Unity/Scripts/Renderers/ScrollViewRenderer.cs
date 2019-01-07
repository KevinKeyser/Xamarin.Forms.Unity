using System;
using System.ComponentModel;

using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
    public class ScrollViewRenderer : ViewRenderer<ScrollView, NativeScrollViewElement>
    {
        public override Transform UnityContainerTransform => NativeElement ? NativeElement.Content : null;

        public ScrollViewRenderer()
        {
            NativeElement.OnScrollChanged += (sender, args) =>
            {
                var x = (1.0 - args.x) * Element.ContentSize.Height;
                var y = (1.0 - args.y) * Element.ContentSize.Height;
                Element.SetScrolledPosition(x, y);
            };
        }

        #region Event Handler
        protected override void OnElementChanged(ElementChangedEventArgs<ScrollView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }
            
            UpdateOrientation();
            UpdateContentSize();
            UpdateScrollXPosition();
            UpdateScrollYPosition();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ScrollView.OrientationProperty.PropertyName ||
                e.PropertyName == ScrollView.VerticalScrollBarVisibilityProperty.PropertyName ||
                e.PropertyName == ScrollView.HorizontalScrollBarVisibilityProperty.PropertyName)
            {
                UpdateOrientation();
            }
            else if (e.PropertyName == ScrollView.ScrollXProperty.PropertyName)
            {
                UpdateScrollXPosition();
            }
            else if (e.PropertyName == ScrollView.ScrollYProperty.PropertyName)
            {
                UpdateScrollYPosition();
            }
            else if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
            {
                UpdateContentSize();
            }

            base.OnElementPropertyChanged(sender, e);
        }
        #endregion

        private void UpdateOrientation()
        {
            switch (Element.Orientation)
            {
                case ScrollOrientation.Vertical:
                    NativeElement.Horizontal = false;
                    NativeElement.Vertical = true;

                    break;

                case ScrollOrientation.Horizontal:
                    NativeElement.Horizontal = true;
                    NativeElement.Vertical = false;

                    break;

                case ScrollOrientation.Both:
                    NativeElement.Horizontal = true;
                    NativeElement.Vertical = true;

                    break;

                default:

                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateScrollXPosition()
        {
            var (width, height) = Element.ContentSize;

            if (width > 0.0)
            {
                NativeElement.ScrollValue =
                    new Vector2((float)(1.0f - Element.ScrollX / width), NativeElement.ScrollValue.y);
            }
        }

        private void UpdateScrollYPosition()
        {
            var (width, height) = Element.ContentSize;

            if (height > 0.0)
            {
                NativeElement.ScrollValue =
                    new Vector2(NativeElement.ScrollValue.x, (float)(1.0f - Element.ScrollY / height));
            }
        }

        private void UpdateContentSize()
        {
            var content = NativeElement.Content;

            var size = Element.ContentSize;
            var x = Element.ScrollX;
            var y = Element.ScrollY;

            var pivot = content.pivot;
            content.anchorMin = new Vector2();
            content.anchorMax = new Vector2();
            content.anchoredPosition = new Vector2();
            content.pivot = new Vector2();

            content.sizeDelta = new Vector2((float)size.Width, (float)size.Height);
            var w = 0.0f;
            var h = 0.0f;

            if (size.Width > 0.0)
            {
                w = (float)(1.0f - x / size.Width);
            }
            else
            {
                NativeElement.Horizontal = false;
            }

            if (size.Height > 0.0)
            {
                h = (float)(1.0f - y / size.Height);
            }
            else
            {
                NativeElement.Vertical = false;
            }

            NativeElement.ScrollValue = new Vector2(w, h);
        }
    }
}