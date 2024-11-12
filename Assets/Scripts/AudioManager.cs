using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    public AudioClip menuMusic;
    public AudioClip gameMusic;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            ChangeMusic(gameMusic);
        }
        else if (scene.name == "Main Menu" || scene.name == "Play Menu")
        {
            ChangeMusic(menuMusic);
        }
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip)
        {
            return;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }
}
