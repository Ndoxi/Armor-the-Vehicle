using UnityEngine;

namespace Codebase.Core
{
    public class LocalResourcesLoader : IResourceLoader
    {
        public T Load<T>(string key) where T : UnityEngine.Object
        {
            return Resources.Load<T>(key);
        }
    }
}