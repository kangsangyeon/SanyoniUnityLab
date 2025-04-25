using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyProject.Examples.ResourceLoaderExample
{
    public class ResourcesProvider : IResourceProvider
    {
        public UniTask<T> LoadAsync<T>(string key) where T : UnityEngine.Object
        {
            return UniTask.RunOnThreadPool(() => Load<T>(key));
        }

        public T Load<T>(string key) where T : UnityEngine.Object
        {
            return Resources.Load<T>(key);
        }

        public void Unload(UnityEngine.Object asset)
        {
            Resources.UnloadAsset(asset);
        }
    }
}