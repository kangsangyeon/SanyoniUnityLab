using UnityEngine;

public class AddressablesPrefab : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    private void Start()
    {
        source.clip = clip;
        source.Play();
    }
}