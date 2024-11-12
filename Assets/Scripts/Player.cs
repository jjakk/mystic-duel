using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    protected Rigidbody2D rigidBody;
    enum Direction { Idle, Left, Right };
    private Direction direction;
    public int moveSpeed;
    public int maxHealth;
    private int health;
    private bool isEnabled;
    // private int highScore;
    public Image healthBar;
    private ScreenShake screenShake;
    [SerializeField] private ParticleSystem explosionParticleSystem = default ;
    [SerializeField] private GameObject smokeEffect1;
    [SerializeField] private GameObject smokeEffect2;
    private int lastScoreSmoke = 0;
    private GameManager gameManager;

    public KeyCode actionKey;

    // Start is called before the first frame update
    void Start() {
        this.reset();
        screenShake = Camera.main.GetComponent<ScreenShake>();
        smokeEffect1.SetActive(false);
        smokeEffect2.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        int currentScore = GameManager.getScore();
        if (currentScore >= lastScoreSmoke + 10)
        {
            smokeEffect1.SetActive(true);
        }

        if (currentScore >= lastScoreSmoke + 50)
        {
            smokeEffect2.SetActive(true);
        }
        
        if(isEnabled) {
            // Use the player-specific action key
            if(Input.GetKeyDown(actionKey)) {
                GameManager.decrementScore();
                this.flipDirection();
            }
        }

        if(direction == Direction.Right) {
            rigidBody.velocity = new Vector2(moveSpeed, 0);
        }
        else if(direction == Direction.Left) {
            rigidBody.velocity = new Vector2(-1 * moveSpeed, 0);
        }
    }
    public void reset() {
        GameManager.resetScore();
        health = maxHealth;
        enable();
        direction = Direction.Idle;
        rigidBody = GetComponent<Rigidbody2D>();
        lastScoreSmoke = 0;
        smokeEffect1.SetActive(false);
        smokeEffect2.SetActive(false);
    }

    public void takeDamage(int damage) {
        health -= damage;
        healthBar.fillAmount = ((float)health / maxHealth);
        explosionParticleSystem.Play();
        screenShake.TriggerShake();
        smokeEffect1.SetActive(false);
        smokeEffect2.SetActive(false);

        lastScoreSmoke = GameManager.getScore();
    }

    public int getHealth() {
        return health;
    }

    public bool getIsEnabled() {
        return isEnabled;
    }

    public void enable() {
        isEnabled = true;
    }

    public void disable() {
        isEnabled = false;
    }

    public void flipDirection() {
        if(direction == Direction.Right) {
            direction = Direction.Left;
        }
        else {
            direction = Direction.Right;
        }
    }
}
