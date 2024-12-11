using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // private AudioSource audioSource;
    public AudioClip hitShieldSoundEffect;
    [SerializeField] private SoundManager soundManager;
    
    void Start()
    {
        // audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Fireball")
        {
            // Debug.Log("Play damn fuckin hit sfx");
            soundManager.PlayShieldHit(hitShieldSoundEffect);
            // audioSource.PlayOneShot(hitShieldSoundEffect);
        }
    }
}
