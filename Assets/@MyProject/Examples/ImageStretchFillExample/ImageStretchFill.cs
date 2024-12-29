using UnityEngine;
using UnityEngine.UI;

public class ImageStretchFill : MonoBehaviour
{
    public Image Image;

    [Range(0, 1)] public float FillAmount;
    public float MinWidth;
    private float CachedFillAmount;
    private float CachedMinWidth;

    private void Awake()
    {
        CachedFillAmount = FillAmount;
        CachedMinWidth = MinWidth;
    }

    private void Update()
    {
        OnValidate();

        bool _shouldUpdate = false;

        if (FillAmount != CachedFillAmount)
        {
            _shouldUpdate = true;
            CachedFillAmount = FillAmount;
        }

        if (MinWidth != CachedMinWidth)
        {
            _shouldUpdate = true;
            CachedMinWidth = MinWidth;
        }

        if (_shouldUpdate)
            UpdateRect();
        _shouldUpdate = false;
    }

    private void UpdateRect()
    {
        var _parentRect = (RectTransform)Image.rectTransform.parent;
        var _parentRectWidth = _parentRect.rect.width;
        Image.rectTransform.offsetMax = new Vector2(-(_parentRectWidth - MinWidth) * (1 - FillAmount), 0);
    }

    private void OnValidate()
    {
        FillAmount = Mathf.Clamp(FillAmount, 0, 1);
        MinWidth = Mathf.Clamp(MinWidth, 0, float.MaxValue);
    }
}