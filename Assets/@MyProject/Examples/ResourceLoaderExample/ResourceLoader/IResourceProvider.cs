using Cysharp.Threading.Tasks;

namespace MyProject.Examples.ResourceLoaderExample
{
    public interface IResourceProvider
    {
        UniTask<T> LoadAsync<T>(string key) where T : UnityEngine.Object;
        T Load<T>(string key) where T : UnityEngine.Object; // 🔸 동기 로딩 추가
        void Unload(UnityEngine.Object asset);
    }
}