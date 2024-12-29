using UnityEngine;
using UnityEngine.UI;

public class CustomUIMeshExample : MonoBehaviour
{
    public Slider fillSlider;
    public Text fillSliderText;
    public CanvasRenderer renderer;

    private void Update()
    {
        fillSliderText.text = $"Fill: {fillSlider.value:F}";
        renderer.GetMaterial().SetFloat("_FillSlider", fillSlider.value);
    }
}