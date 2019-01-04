using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    internal class ResourcesProvider : ISystemResourcesProvider
    {
        private ResourceDictionary dictionary;

        public IResourceDictionary GetSystemResources()
        {
            dictionary = new ResourceDictionary();

            return dictionary;
        }
    }
}