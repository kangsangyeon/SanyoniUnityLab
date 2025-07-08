using Sonity;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SonityMixerExample : MonoBehaviour
{
    public Slider VoiceSlider;
    public Slider SfxSlider;
    public Slider BgmSlider;
    public Slider MasterSlider;
    public AudioMixer Mixer;

    private void Start()
    {
        VoiceSlider.onValueChanged.AddListener(x => Mixer.SetFloat("VoiceVolume", ValueToVolume(x)));
        SfxSlider.onValueChanged.AddListener(x => Mixer.SetFloat("SfxVolume", ValueToVolume(x)));
        BgmSlider.onValueChanged.AddListener(x => Mixer.SetFloat("BgmVolume", ValueToVolume(x)));
        MasterSlider.onValueChanged.AddListener(x => Mixer.SetFloat("MasterVolume", ValueToVolume(x)));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var _se = Resources.Load<SoundEvent>("Bgm/good_SE");
            _se.MusicPlay();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            var _se = Resources.Load<SoundEvent>("Voice/audio_hello_SE");
            _se.Play(transform);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            var _se = Resources.Load<SoundEvent>("Sfx/ESM_Ancient_Game_Weapon_Swing_Blood_Hit_SE");
            _se.Play(transform);
        }
    }

    public float ValueToVolume(float value)
    {
        // https://stackoverflow.com/questions/46529147/how-to-set-a-mixers-volume-to-a-sliders-volume-in-unity
        float _clampValue = Mathf.Clamp(value, 0.0001f, 1f); // value가 0이 되면 볼륨 크기 조절이 이상해지기 때문에 이를 막음.
        float _logValue = Mathf.Log10(_clampValue);
        return _logValue * 20;
    }
}