using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MackySoft.Navigathena.SceneManagement;
using UnityEngine;

public class NotUsedEntryPoint : SceneEntryPointBase
{
    protected override UniTask OnEnter(ISceneDataReader reader, CancellationToken cancellationToken)
    {
        Debug.LogError("Not Used Scene!!");
        return base.OnEnter(reader, cancellationToken);
    }
    
    
}