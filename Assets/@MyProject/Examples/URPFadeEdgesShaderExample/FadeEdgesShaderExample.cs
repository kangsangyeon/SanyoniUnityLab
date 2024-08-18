using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeEdges : MonoBehaviour
{
    public Image Fader;
    public RectTransform GroupRect;
    public CanvasGroup Group;
    public Button ReplayButton;

    private void Start()
    {
        Fader.material = new Material(Fader.material);
        ReplayButton.onClick.AddListener(Replay);
        Replay();
    }

    public void Replay()
    {
        DOTween.Kill(Fader, true);
        Fader.material.SetFloat("_FadeLeft", -0.4f);
        Fader.material.SetFloat("_FadeRight", -0.4f);
        Fader.material.SetFloat("_AdditionalBrightness", 10f);
        Fader.material.SetFloat("_Alpha", 1f);
        Fader.rectTransform.localScale = new Vector3(.3f, 1f, 1f);
        Group.alpha = 1f;
        GroupRect.localScale = new Vector3(.3f, .3f, 1f);
        DOTween.Sequence()
            .Append(Fader.material.DOFloat(0f, "_FadeLeft", .1f))
            .Join(Fader.material.DOFloat(0f, "_FadeRight", .1f))
            .Join(Fader.rectTransform.DOScaleX(1f, .3f))
            .Join(GroupRect.DOScale(Vector3.one, .2f))
            .Join(DOTween.Sequence()
                .AppendInterval(.1f)
                .Append(Fader.material.DOFloat(0f, "_AdditionalBrightness", .2f))
                .Join(Fader.material.DOFloat(0f, "_Alpha", .2f)))
            .AppendInterval(2f)
            .Append(Group.DOFade(0f, .2f))
            .Join(GroupRect.DOScaleY(1.1f, .2f))
            .SetEase(Ease.OutQuad)
            .SetId(Fader);
    }
}