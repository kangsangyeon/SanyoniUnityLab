using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace MyProject.Examples.ResourceLoaderExample
{
    public class AddressablesProvider : IResourceProvider
    {
        public async UniTask<T> LoadAsync<T>(string key) where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            await handle.ToUniTask();
            return handle.Result;
        }

        public T Load<T>(string key) where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(key);

            if (!handle.IsDone)
            {
                // WaitForCompletion은 초기화가 끝났고, 로컬 리소스일 때만 유효
                Addressables.InitializeAsync().WaitForCompletion(); // 보통 이게 이미 호출되어 있어야 안전
                handle.WaitForCompletion();
            }

            return handle.Result;
        }

        public void Unload(UnityEngine.Object asset)
        {
            Addressables.Release(asset);
        }
    }
}