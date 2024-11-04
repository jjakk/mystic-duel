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
    public void ClickAndLoad(){
        Invoke("LoadGame", 0.3f);
    }
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }
    public void BackToMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
