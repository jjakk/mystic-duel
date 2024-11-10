using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // Player
    [SerializeField]private GameObject playerObj;
    private Player player;

    //Player 2
    [SerializeField] private GameObject secondPlayerObj;
    private Player secondPlayer;
    public GameObject secondPlayerHealthBar;


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

    public static bool isMultiplayer = false;
    private bool isGameOver = false;



    void Start() {
        player = playerObj.GetComponent<Player>();
        player.actionKey = KeyCode.Space;
        player.reset(); 

        if (isMultiplayer) {
            secondPlayerObj.SetActive(true);
            secondPlayer = secondPlayerObj.GetComponent<Player>();
            secondPlayer.actionKey = KeyCode.Return;
            secondPlayerHealthBar.SetActive(true);
            secondPlayer.reset();
        } else {
            secondPlayerObj.SetActive(false);
        }

        Resume();
}

    void Update() {
        scoreText.text = score.ToString();
        if (!isGameOver && (player.getHealth() == 0 || (isMultiplayer && secondPlayer.getHealth() == 0))) {
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
        if (isMultiplayer) {
            secondPlayer.disable();
        }
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        player.enable();
        if (isMultiplayer) {
            secondPlayer.enable();
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver() {
        isGameOver = true;
        gameOverMenu.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        player.disable();
        if (isMultiplayer) {
            secondPlayer.disable();
        }
        if (isMultiplayer) {
            if (player.getHealth() == 0) {
                finalScore.text = "Player 2 Wins!\nFinal Score: " + score.ToString();
            } else if (secondPlayer.getHealth() == 0) {
                finalScore.text = "Player 1 Wins!\nFinal Score: " + score.ToString();
            }
        } else {
            finalScore.text = "Game Over\nYour Score: " + score.ToString() + "\nHigh Score: " + highScore.ToString();
        }
    }

    public void LoadMainMenu() {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayAgain() {
        player.reset();
        if (isMultiplayer) {
            secondPlayer.reset();
        }
        Time.timeScale = 1;
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
