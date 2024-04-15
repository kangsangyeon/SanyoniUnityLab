using UnityEngine;

public class SpatialFader : MonoBehaviour
{
    private Transform Listener;
    private AudioSource source;

    private float sqrMinDist;
    private float sqrMaxDist;

    void Start()
    {
        Listener = FindObjectOfType<AudioListener>().transform;
        source = GetComponent<AudioSource>();
        if (!source) return;

        sqrMinDist = source.minDistance * source.minDistance;
        sqrMaxDist = source.maxDistance * source.maxDistance;
    }

    void Update()
    {
        if (!source || !Listener)
            return;
        var t = (transform.position - Listener.position).sqrMagnitude;
        t = (t - sqrMinDist) / (sqrMaxDist - sqrMinDist);
        source.spatialBlend = t * 5;
    }
}