using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
    public interface IImageSourceHandler : IRegisterable
    {
        Task<Texture> LoadImageAsync(ImageSource imagesource,
                                     CancellationToken cancelationToken = default(CancellationToken));
    }

    public sealed class FileImageSourceHandler : IImageSourceHandler
    {
        public Task<Texture> LoadImageAsync(ImageSource imagesource,
                                            CancellationToken cancelationToken = default(CancellationToken))
        {
            Texture2D texture = null;

            if (imagesource is FileImageSource filesource)
            {
                var file = filesource.File;
                var data = File.ReadAllBytes(file);
                texture = new Texture2D(1, 1);
                texture.LoadImage(data);
            }

            return Task.FromResult((Texture)texture);
        }
    }

    public sealed class StreamImageSourceHandler : IImageSourceHandler
    {
        public async Task<Texture> LoadImageAsync(ImageSource imagesource,
                                                  CancellationToken cancelationToken = new CancellationToken())
        {
            if (imagesource is StreamImageSource streamImageSource &&
                streamImageSource.Stream != null)
            {
                var memoryStream = new MemoryStream();

                return await ((IStreamImageSource)streamImageSource)
                   .GetStreamAsync(cancelationToken).Result
                   .CopyToAsync(memoryStream)
                   .ContinueWith(
                        (stream) =>
                        {
                            var texture = new Texture2D(1, 1);
                            texture.LoadImage(memoryStream.ToArray());

                            return (Texture)texture;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            return null;
        }
    }

    public sealed class UriImageSourceHandler : IImageSourceHandler
    {
        public async Task<Texture> LoadImageAsync(
            ImageSource imagesource, CancellationToken cancelationToken = new CancellationToken())
        {
            if (imagesource is UriImageSource imageLoader &&
                imageLoader.Uri != null)
            {
                var client = new WebClient();

                return await client.DownloadDataTaskAsync(imageLoader.Uri)
                                   .ContinueWith((data) =>
                                    {
                                        Texture2D texture = new Texture2D(1, 1);
                                        texture.LoadImage(data.Result);

                                        return texture;
                                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            return null;
        }
    }
}