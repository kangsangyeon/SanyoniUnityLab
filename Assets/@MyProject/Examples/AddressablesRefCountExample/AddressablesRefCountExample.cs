using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesRefCountExample : MonoBehaviour
{
    private AsyncOperationHandle<GameObject> _prefabHandle;
    private GameObject _instance;

    // 1단계: 프리팹 로드 및 Acquire
    [ContextMenu("Step 1 - Load & Acquire")]
    public async void Step1_LoadAndAcquire()
    {
        _prefabHandle = Addressables.LoadAssetAsync<GameObject>("my_prefab"); // Addressables 키를 사용하세요
        await _prefabHandle.Task;

        Debug.Log("Step1: Prefab loaded.");
        Debug.Log("Step1: Acquiring reference manually.");
        Addressables.ResourceManager.Acquire(_prefabHandle); // 참조 카운트 +1

        Debug.Log("Step1: Instantiating prefab.");
        _instance = Instantiate(_prefabHandle.Result);
    }

    // 2단계: 첫 번째 Release
    [ContextMenu("Step 2 - First Release")]
    public void Step2_ReleaseOnce()
    {
        Debug.Log("Step2: Destroying instance.");
        Destroy(_instance); // 인스턴스만 파괴

        Debug.Log("Step2: Releasing 1st time.");
        Addressables.Release(_prefabHandle); // 참조 카운트 -1
    }

    // 3단계: 두 번째 Release (리소스가 이 시점에 언로드됨)
    [ContextMenu("Step 3 - Second Release")]
    public void Step3_ReleaseTwice()
    {
        Debug.Log("Step3: Releasing 2nd time.");
        Addressables.Release(_prefabHandle); // 참조 카운트 -1 → 이제 0이 되므로 언로드
    }
}