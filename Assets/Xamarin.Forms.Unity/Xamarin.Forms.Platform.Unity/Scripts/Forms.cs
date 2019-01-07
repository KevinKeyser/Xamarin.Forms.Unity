using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

using Xamarin.Forms.Internals;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.Forms.Platform.Unity.ResourcesProvider))]


namespace Xamarin.Forms.Platform.Unity
{
    public static class Forms
    {
        public static bool IsInitialized { get; private set; }

        public static Thread MainThread { get; private set; }

        public static void Init(IEnumerable<Assembly> rendererAssemblies = null)
        {
            if (IsInitialized)
            {
                return;
            }
            
            Log.Listeners.Add(new XamarinLogListener());
            Internals.Registrar.ExtraAssemblies = rendererAssemblies?.ToArray();
            
            
            MainThread = Thread.CurrentThread;

            Device.SetIdiom(TargetIdiom.Desktop);
            Device.PlatformServices = new UnityPlatformServices();
            Device.Info = new UnityDeviceInfo();
            ExpressionSearch.Default = new UnityExpressionSearch();

            Registrar.RegisterAll(
                new[]
                {
                    typeof(ExportRendererAttribute),
                    typeof(ExportCellAttribute),
                    typeof(ExportImageSourceHandlerAttribute)
                });
            
            IsInitialized = true;
        }
    }
}