using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MackySoft.Navigathena;
using MackySoft.Navigathena.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HomeEntryPoint : SceneEntryPointBase
{
    private static readonly ISceneIdentifier k_GameSceneIdentifier =
        new BuiltInSceneIdentifier("NavigathenaExample_Game");

    [SerializeField] private Button _loadGameButton;
    private IDisposable _subscriptionDisposable;

    protected override UniTask OnInitialize(
        ISceneDataReader reader,
        IProgress<IProgressDataStore> progress,
        CancellationToken cancellationToken)
    {
        CancellationTokenSource a = new();
        var b = a.Token;
        a.Cancel();
        return UniTask.Delay(5000, cancellationToken: cancellationToken);
    }

    protected override UniTask OnFinalize(
        ISceneDataWriter writer,
        IProgress<IProgressDataStore> progress,
        CancellationToken cancellationToken)
    {
        return UniTask.Delay(5000, cancellationToken: cancellationToken);
    }

    protected override UniTask OnEnter(
        ISceneDataReader reader,
        CancellationToken cancellationToken)
    {
        _subscriptionDisposable =
            _loadGameButton.OnClickAsAsyncEnumerable().SubscribeAwait(_ => OnLoadGameButtonClicked());
        // _loadGameButton.OnClickAsAsyncEnumerable().SubscribeAwait(_ =>
        // {
        //     Debug.Log("hello world");
        //     return UniTask.CompletedTask;
        // });
        return base.OnEnter(reader, cancellationToken);
    }

    protected override UniTask OnExit(
        ISceneDataWriter writer,
        CancellationToken cancellationToken)
    {
        _subscriptionDisposable.Dispose();
        return base.OnExit(writer, cancellationToken);
    }

    private async UniTask OnLoadGameButtonClicked()
    {
        await GlobalSceneNavigator.Instance.Push(k_GameSceneIdentifier);
    }
}