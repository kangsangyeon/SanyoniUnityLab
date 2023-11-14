using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadAddressables : MonoBehaviour
{
    public string key;
    public AsyncOperationHandle<GameObject> opHandle;

    public IEnumerator Load()
    {
        opHandle = Addressables.LoadAssetAsync<GameObject>(key);
        yield return opHandle;

        // if (opHandle.Status == AsyncOperationStatus.Succeeded)
        // {
        //     GameObject obj = opHandle.Result;
        //     Instantiate(obj, transform);
        // }
    }

    void OnDestroy()
    {
        Addressables.Release(opHandle);
    }
}