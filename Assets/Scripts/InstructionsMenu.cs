using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }
}
