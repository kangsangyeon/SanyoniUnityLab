using UnityEngine;

public class DummyAudioService : IAudioService
{
    public override void PlayOneShot(AudioClip _clip)
    {
        Debug.Log("DummyAudioService::PlayOneShot() called");
    }

    public override void PlayLoop(AudioClip _clip)
    {
        Debug.Log("DummyAudioService::PlayLoop() called");
    }
}