using UnityEngine;
using Zenject;

public class ExtenjectExample : MonoBehaviour
{
    [SerializeField] private AudioClip m_OneShotClip;
    [SerializeField] private AudioClip m_LoopClip;

    private IAudioService m_Audio;

    [Inject]
    private void Construct(IAudioService _audio)
    {
        m_Audio = _audio;
        Debug.Log(m_Audio);
    }

    public void OnClickPlayOneShot() => m_Audio.PlayOneShot(m_OneShotClip);
    public void OnClickPlayLoop() => m_Audio.PlayLoop(m_LoopClip);
}