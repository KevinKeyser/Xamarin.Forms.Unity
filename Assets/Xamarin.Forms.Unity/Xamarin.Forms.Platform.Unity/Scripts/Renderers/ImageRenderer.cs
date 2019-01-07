using System;
using System.ComponentModel;

using UnityEngine;
using UnityEngine.UI;

namespace Xamarin.Forms.Platform.Unity
{
    public class ImageRenderer : ViewRenderer<Image, NativeImageElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }
            
            SetImage(e.OldElement);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Image.SourceProperty.PropertyName)
            {
                SetImage();
            }
            else if (e.PropertyName == Image.AspectProperty.PropertyName) { }
            else if (e.PropertyName == Image.IsOpaqueProperty.PropertyName) { }
            else if (e.PropertyName == Image.IsLoadingProperty.PropertyName) { }

            base.OnElementPropertyChanged(sender, e);
        }

        private async void SetImage(Image oldElement = null)
        {
            var source = Element.Source;

            if (oldElement != null)
            {
                var oldSource = oldElement.Source;

                if (Equals(oldSource, source))
                {
                    return;
                }

                if (oldSource is FileImageSource fileImageSource &&
                    source is FileImageSource imageSource &&
                    fileImageSource.File == imageSource.File)
                {
                    return;
                }

                NativeElement.Texture = null;
            }

            Element.SetIsLoading(true);

            IImageSourceHandler handler;

            ((IImageController)Element).SetIsLoading(true);

            if (source != null
                && (handler = Internals.Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(source)) != null)
            {
                Texture image;

                try
                {
                    image = await handler.LoadImageAsync(source);
                }
                catch (OperationCanceledException)
                {
                    image = null;
                    Internals.Log.Warning("Image loading", "Image load cancelled");
                }
                catch (Exception ex)
                {
                    image = null;
                    Internals.Log.Warning("Image loading", $"Image load failed: {ex}");
                }

                var imageView = NativeElement;

                if (imageView != null)
                    imageView.Texture = image;

                ((IVisualElementController)Element).NativeSizeChanged();
            }
            else
                NativeElement.Texture = null;

            ((IImageController)Element).SetIsLoading(false);
        }
    }
}