using UnityEngine;

namespace Codebase.Core
{
    public interface IResourceLoader
    {
        public T Load<T>(string key) where T : Object;
    }
}