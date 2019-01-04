using System;
using System.Collections;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    public class UnityPlatformServices : IPlatformServices
    {
        private static readonly MD5CryptoServiceProvider checksum = new MD5CryptoServiceProvider();

        public bool IsInvokeRequired => Forms.MainThread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId;

        public string RuntimePlatform => "Unity";

        public void BeginInvokeOnMainThread(Action action)
        {
            SynchronizationContext.Current.Post(_ => action(), null);
        }

        public Ticker CreateTicker()
        {
            return new UnityTicker();
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public string GetMD5Hash(string input)
        {
            int Hex(int value)
            {
                if (value < 10)
                {
                    return '0' + value;
                }

                return 'a' + value - 10;
            }

            var bytes = checksum.ComputeHash(Encoding.UTF8.GetBytes(input));
            var ret = new char[32];

            for (var i = 0; i < 16; i++)
            {
                ret[i * 2] = (char)Hex(bytes[i] >> 4);
                ret[i * 2 + 1] = (char)Hex(bytes[i] & 0xf);
            }

            return new string(ret);
        }


        public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
        {
            switch (size)
            {
                case NamedSize.Micro:
                    return 12;
                
                case NamedSize.Default: // Unity Defaults their Text/Buttons etc. to 14px
                case NamedSize.Small:
                    return 14;
                
                case NamedSize.Medium:
                    return 17;
                
                case NamedSize.Large:
                    return 22;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }
        }

        public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<Stream>();

            try
            {
                var request = WebRequest.CreateHttp(uri);

                request.BeginGetResponse(ar =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        taskCompletionSource.SetCanceled();

                        return;
                    }

                    try
                    {
                        var stream = request.EndGetResponse(ar).GetResponseStream();
                        taskCompletionSource.TrySetResult(stream);
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }

            return taskCompletionSource.Task;
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            return new UnityIsolatedStorageFile(IsolatedStorageFile.GetUserStoreForAssembly());
        }

        public void OpenUriAction(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
            {
                return;
            }

            UnityEngine.Application.OpenURL(uri.AbsoluteUri);
        }

        public void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            CoroutineManager.Instance.StartCoroutine(TimerCoroutine((float)interval.TotalSeconds, callback));
        }

        private static IEnumerator TimerCoroutine(float secondsInterval, Func<bool> callback)
        {
            do
            {
                yield return new WaitForSeconds(secondsInterval);
            } while (!callback());
        }
        
        public void QuitApplication()
        {
            UnityEngine.Application.Quit();
        }
    }
}