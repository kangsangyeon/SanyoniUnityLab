using UnityEngine;

public abstract class IAudioService : MonoBehaviour
{
    public abstract void PlayOneShot(AudioClip _clip);
    public abstract void PlayLoop(AudioClip _clip);
}