using UnityEngine;

namespace Xamarin.Forms.Platform.Unity
{
    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager instance;

        private static readonly object @lock = new object();

        private static bool applicationIsQuitting;

        public static CoroutineManager Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    return null;
                }

                lock (@lock)
                {
                    if (instance != null)
                    {
                        return instance;
                    }

                    instance = FindObjectOfType<CoroutineManager>();

                    if (instance != null)
                    {
                        return instance;
                    }


                    var singleton = new GameObject();
                    instance = singleton.AddComponent<CoroutineManager>();
                    singleton.name = $"(singleton) {nameof(CoroutineManager)}";

                    DontDestroyOnLoad(singleton);

                    return instance;
                }
            }
        }

        public virtual void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}