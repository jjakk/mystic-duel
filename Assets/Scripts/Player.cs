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
    private DamageFlash _damageFlash;
    private bool isEnabled;
    // private int highScore;
    public Image healthBar;
    private ScreenShake screenShake;
    private ScreenShake backgroundShake;
    [SerializeField] private GameObject backgroundObject = default ;
    [SerializeField] private ParticleSystem explosionParticleSystem = default ;
    [SerializeField] private GameObject smokeEffect10;
    [SerializeField] private GameObject smokeEffect50;
    [SerializeField] private GameObject smokeEffect100;
    private int lastScoreSmoke = 0;
    private GameManager gameManager;

    //Audio variables
    private AudioSource audioSource;
    private AudioSource audioSourceMove;
    public AudioClip powerUpSoundEffect;
    public AudioClip takeDamageSoundEffect;
    public AudioClip moveSoundEffect;
    public AudioClip moveSoundEffect2;
    public AudioClip gainLifeSoundEffect;

    //Flash
    private SimpleDamageFlash simpleFlash;

    public KeyCode actionKey;

    // Start is called before the first frame update
    void Start() {
        this.reset();
        screenShake = Camera.main.GetComponent<ScreenShake>();
        backgroundShake = backgroundObject.GetComponent<ScreenShake>();
        smokeEffect10.SetActive(false);
        smokeEffect50.SetActive(false);
        smokeEffect100.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        _damageFlash = GetComponent<DamageFlash>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSourceMove = gameObject.AddComponent<AudioSource>();
        audioSourceMove.volume = 0.2f;

        simpleFlash = gameObject.GetComponent<SimpleDamageFlash>();
    }

    // Update is called once per frame
    void Update() {
        int currentScore = GameManager.getScore();
        if (currentScore >= lastScoreSmoke + 15)
        {
            smokeEffect10.SetActive(true);
            // audioSource.PlayOneShot(powerUpSoundEffect);
        }

        if (currentScore >= lastScoreSmoke + 50)
        {
            smokeEffect50.SetActive(true);
            // audioSource.PlayOneShot(powerUpSoundEffect);
        }

        if (currentScore >= lastScoreSmoke + 100)
        {
            smokeEffect10.SetActive(false);
            smokeEffect100.SetActive(true);
            // audioSource.PlayOneShot(powerUpSoundEffect);
        }
        
        if(isEnabled) {
            // Use the player-specific action key
            if(Input.GetKeyDown(actionKey)) {
                GameManager.decrementScore();
                this.flipDirection();
                audioSourceMove.PlayOneShot(moveSoundEffect);
                audioSourceMove.PlayOneShot(moveSoundEffect2);
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
        smokeEffect10.SetActive(false);
        smokeEffect50.SetActive(false);
        smokeEffect100.SetActive(false);
    }

    public void takeDamage(int damage) {
        health -= damage;
        if(health > maxHealth) {
            health = maxHealth;
        }

        if(damage < 0)
        {
            audioSource.PlayOneShot(gainLifeSoundEffect);
        }

        healthBar.fillAmount = ((float)health / maxHealth);
        
        if(damage > 0) {
            audioSource.PlayOneShot(takeDamageSoundEffect);
            simpleFlash.Flash();
            //_damageFlash.CallDamageFlash();
            explosionParticleSystem.Play();
            screenShake.TriggerShake();
            backgroundShake.TriggerShake();
            smokeEffect10.SetActive(false);
            smokeEffect50.SetActive(false);
            smokeEffect100.SetActive(false);
        }

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

        GetComponent<SpriteRenderer>().flipX = (direction == Direction.Left);
    }
}
