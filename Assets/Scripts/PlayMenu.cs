using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ClickAndLoadSinglePlayer(){
        GameManager.isMultiplayer = false;  // Set to single-player mode
        Invoke("LoadGame", 0.3f);
    }

    public void ClickAndLoadMultiplayer(){
        GameManager.isMultiplayer = true;   // Set to multiplayer mode
        Invoke("LoadGame", 0.3f);
    }

    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
