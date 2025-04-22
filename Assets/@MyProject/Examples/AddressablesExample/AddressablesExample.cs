using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesExample : MonoBehaviour
{
    public AssetReference reference;
    public AssetReference reference2;
    public string prefabAddress = "Prefab/Sfx";
    public AssetReference prefabReference;
    public AudioSource source;

    private void Start()
    {
        Method().Forget();
    }

    private async UniTask Method()
    {
        var WAITING_DURATION = 3;

        {
            var _clip = await reference.LoadAssetAsync<AudioClip>().Task;
            source.clip = _clip;
            source.Play();
            await UniTask.WaitForSeconds(WAITING_DURATION);
            reference.ReleaseAsset();
            Debug.Log("release clip 1");
        }

        {
            var _handle = reference2.LoadAssetAsync<AudioClip>();
            await _handle.Task;
            source.clip = _handle.Result;
            source.Play();
            await UniTask.WaitForSeconds(WAITING_DURATION);
            reference2.ReleaseAsset();
            Debug.Log("release clip 2");
        }

        {
            var _instance = Addressables.InstantiateAsync(prefabAddress);
            await UniTask.WaitForSeconds(WAITING_DURATION);
            Addressables.ReleaseInstance(_instance);
            Debug.Log("release prefab instance");
        }

        {
            var _instance = await prefabReference.InstantiateAsync().Task;
            prefabReference.ReleaseInstance(_instance);
            Debug.Log("release prefab instance");
        }
    }
}