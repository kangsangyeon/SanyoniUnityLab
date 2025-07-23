using System.Threading;
using Cysharp.Threading.Tasks;
using MackySoft.Navigathena.SceneManagement;

public class GameEntryPoint : SceneEntryPointBase
{
    protected override UniTask OnEnter(ISceneDataReader reader, CancellationToken cancellationToken)
    {
        return base.OnEnter(reader, cancellationToken);
    }

    protected override UniTask OnExit(ISceneDataWriter writer, CancellationToken cancellationToken)
    {
        return base.OnExit(writer, cancellationToken);
    }
}