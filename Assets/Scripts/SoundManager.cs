using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayShieldHit(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayCoinSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
