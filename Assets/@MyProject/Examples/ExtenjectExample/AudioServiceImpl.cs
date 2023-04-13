using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioServiceImpl : IAudioService
{
    [SerializeField] private List<AudioSource> m_AudioSourceList;

    public override void PlayOneShot(AudioClip _clip)
    {
        Debug.Log("AudioServiceImpl::PlayOneShot() called");

        var _audioSource = m_AudioSourceList.First(source => source.isPlaying == false);
        _audioSource.PlayOneShot(_clip);
    }

    public override void PlayLoop(AudioClip _clip)
    {
        Debug.Log("AudioServiceImpl::PlayLoop() called");

        var _audioSource = m_AudioSourceList.First(source => source.isPlaying == false);
        _audioSource.loop = true;
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
}