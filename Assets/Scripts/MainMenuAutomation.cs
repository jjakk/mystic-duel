using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainMenuAutomation : MonoBehaviour
{
    public Volume postProcessingVolume;
    public float sineWaveSpeed = 1f;
    public float scatterMin = 0.2f;
    public float scatterMax = 0.8f;

    private Bloom bloom;

    void Start()
    {
        if (postProcessingVolume.profile.TryGet(out bloom))
        {
        }
    }

    void Update()
    {
        float scatterValue = Mathf.Lerp(scatterMin, scatterMax, (Mathf.Sin(Time.time * sineWaveSpeed) + 1f) / 2f);
        bloom.scatter.value = scatterValue;
    }
}
