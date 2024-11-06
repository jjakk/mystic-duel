using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // Player
    [SerializeField]private GameObject playerObj;
    private Player player;
    // Canvases
    [SerializeField]private GameObject pauseMenu;
    [SerializeField]private GameObject gameOverMenu;
    [SerializeField]private GameObject hud;
    // Scoring text outputs
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private TextMeshProUGUI finalScore;
    // Score variables
    private static int score;
    private static int highScore;

    void Start() {
        player = playerObj.GetComponent<Player>();
        Resume();
    }

    void Update() {
        scoreText.text = score.ToString();
        if(player.getHealth() == 0) {
            GameOver();
        }
        if(Input.GetKey(KeyCode.P)) {
            Pause();
        }
    }
    // Score Methods
    public static void incrementScore() {
        score += 1;
        if(score > highScore) {
            highScore = score;
        }
    }
    public static void decrementScore() {
        score -= 1;
        if(score < 0) {
            score = 0;
        }
    }
    public static void resetScore() {
        score = 0;
    }
    public static int getScore() {
        return score;
    }
    // Navigation Methods
    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        player.disable();
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        player.enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver() {
        gameOverMenu.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        player.disable();
        finalScore.text = "Your Score: " + score.ToString() + "\n High Score: " + highScore.ToString();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayAgain() {
        player.reset();
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
