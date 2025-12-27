using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Examples.UniTaskDOTweenCancellationTokenExample
{
    public class UniTaskDOTweenCancellationTokenExample : MonoBehaviour
    {
        public GameObject Target;
        private CancellationTokenSource _cts = new();

        public void OnClickSequence()
        {
            RenewCts();
            AnimationSequence(_cts.Token).Forget();
        }

        public void OnClickSeperateAwaits()
        {
            RenewCts();
            AnimationSeperateAwaits(_cts.Token).Forget();
        }

        public void OnClickSeperateAwaitsSuppressCancellationThrow()
        {
            RenewCts();
            AnimationSeperateAwaitsSuppressCancellationThrow(_cts.Token).Forget();
        }

        private void RenewCts()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new();
        }

        private UniTask AnimationSequence(CancellationToken cancellationToken)
        {
            if (Target == null)
                return UniTask.CompletedTask;

            var startPos = Target.transform.position;
            const float duration = 2f;

            var p1 = startPos + Vector3.right * 2f;
            var p2 = p1 + Vector3.up * 2f;
            var p3 = p2 + Vector3.left * 2f;
            var p4 = p3 + Vector3.down * 2f; // should equal startPos

            var seq = DOTween.Sequence();
            seq.Append(Target.transform.DOMove(p1, duration));
            seq.Append(Target.transform.DOMove(p2, duration));
            seq.Append(Target.transform.DOMove(p3, duration));
            seq.Append(Target.transform.DOMove(p4, duration));

            // Return a UniTask that awaits sequence completion and respects cancellation.
            return seq.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete);
        }

        private async UniTask AnimationSeperateAwaits(CancellationToken cancellationToken)
        {
            if (Target == null)
                return;

            var startPos = Target.transform.position;
            const float duration = 2f;

            var p1 = startPos + Vector3.right * 2f;
            var p2 = p1 + Vector3.up * 2f;
            var p3 = p2 + Vector3.left * 2f;
            var p4 = p3 + Vector3.down * 2f; // should equal startPos

            var t1 = Target.transform.DOMove(p1, duration);
            await t1.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete);
            Debug.Log("t1 end");

            var t2 = Target.transform.DOMove(p2, duration);
            await t2.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete);
            Debug.Log("t2 end");

            var t3 = Target.transform.DOMove(p3, duration);
            await t3.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete);
            Debug.Log("t3 end");

            var t4 = Target.transform.DOMove(p4, duration);
            await t4.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete);
            Debug.Log("t4 end");

            // 중간에 cancel하면 끊겨버린다..
        }

        private async UniTask AnimationSeperateAwaitsSuppressCancellationThrow(CancellationToken cancellationToken)
        {
            if (Target == null)
                return;

            var startPos = Target.transform.position;
            const float duration = 2f;

            var p1 = startPos + Vector3.right * 2f;
            var p2 = p1 + Vector3.up * 2f;
            var p3 = p2 + Vector3.left * 2f;
            var p4 = p3 + Vector3.down * 2f; // should equal startPos

            var t1 = Target.transform.DOMove(p1, duration);
            await t1.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete).SuppressCancellationThrow();
            Debug.Log("t1 end");

            var t2 = Target.transform.DOMove(p2, duration);
            await t2.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete).SuppressCancellationThrow();
            Debug.Log("t2 end");

            var t3 = Target.transform.DOMove(p3, duration);
            await t3.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete).SuppressCancellationThrow();
            Debug.Log("t3 end");

            var t4 = Target.transform.DOMove(p4, duration);
            await t4.ToUniTask(cancellationToken: cancellationToken, tweenCancelBehaviour: TweenCancelBehaviour.Complete).SuppressCancellationThrow();
            Debug.Log("t4 end");
        }
    }
}