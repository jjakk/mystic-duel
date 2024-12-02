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

    // Screen Shake variables
    private ScreenShake screenShake;
    private ScreenShake backgroundShake;


    [SerializeField] private GameObject backgroundObject = default ;
    [SerializeField] private ParticleSystem explosionParticleSystem = default ;
    
    //Smoke variables
    [SerializeField] private GameObject smokeEffect10;
    [SerializeField] private GameObject smokeEffect50;
    [SerializeField] private GameObject smokeEffect100;
    private int lastScoreSmoke = 0;
    
    //Game manager
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

    //Shield Variables
    private bool shieldActive = false;
    [SerializeField] private float shieldDuration = 0.5f;
    [SerializeField] private GameObject shieldVisual;
    private Coroutine shieldCoroutine; // Variable to track/stop the shield timer
    [SerializeField] private ParticleSystem ps;

    //Idk what this is --pedro
    //I have added it to have seperate keys to use the multi player --Kushagra 
    public KeyCode actionKey;

    // Start is called before the first frame update
    void Start() {
        //this.reset();
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
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(moveSpeed, 0);

        //shieldDuration = (float)ps.main.startLifetime;
        //Debug.Log("Shield Duration: ", shieldDuration.ToString());
        
    }

    // Update is called once per frame
    void Update() {
        int currentScore = GameManager.getScore();
        if (currentScore >= lastScoreSmoke + 15)
        {
            Debug.Log("15 points wo taking damage");
            smokeEffect10.SetActive(true);
            // audioSource.PlayOneShot(powerUpSoundEffect);
        }

        if (currentScore >= lastScoreSmoke + 50)
        {
            Debug.Log("50 points wo taking damage");
            smokeEffect50.SetActive(true);
            // audioSource.PlayOneShot(powerUpSoundEffect);
        }

        if (currentScore >= lastScoreSmoke + 100)
        {
            Debug.Log("100 points wo taking damage");
            smokeEffect10.SetActive(false);
            smokeEffect100.SetActive(true);
            screenShake.StartContinuousShake(0.05f);
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

        if (direction == Direction.Right) {
            rigidBody.velocity = new Vector2(moveSpeed, 0);
        }
        else if (direction == Direction.Left) {
            rigidBody.velocity = new Vector2(-1 * moveSpeed, 0);
        }
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            activateShield();
        }
    }
    public void reset(Vector2 initialVelocity) {
        GameManager.resetScore();
        health = maxHealth;
        enable();
        direction = initialVelocity.x > 0 ? Direction.Right : Direction.Left;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = initialVelocity;
        lastScoreSmoke = 0;
        smokeEffect10.SetActive(false);
        smokeEffect50.SetActive(false);
        smokeEffect100.SetActive(false);
        screenShake.StopContinuousShake();
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
            if (shieldActive)
            {
                Debug.Log("Shield absorbed the damage!");
                deactivateShield();
                return;
            }
            
            audioSource.PlayOneShot(takeDamageSoundEffect);
            simpleFlash.Flash();
            explosionParticleSystem.Play();
            screenShake.TriggerShake();
            //backgroundShake.TriggerShake();

            Debug.Log("Took damage");
            smokeEffect10.SetActive(false);
            smokeEffect50.SetActive(false);
            smokeEffect100.SetActive(false);
            screenShake.StopContinuousShake();
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

    public void activateShield()
    {
        if (shieldActive) return;
        
        shieldActive = true;
        Debug.Log("Shield activated");
        
        shieldVisual.SetActive(true);

        // Start coroutine to deactivate the shield after the duration
        shieldCoroutine = StartCoroutine(shieldTimer());
    }

    private IEnumerator shieldTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        deactivateShield();
    }

    public void deactivateShield()
    {
        shieldActive = false;
        Debug.Log("Shield deactivated");

        shieldVisual.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Rigidbody2D rbOther = collision.rigidbody;
        Rigidbody2D rbSelf = GetComponent<Rigidbody2D>();

        if (rbOther != null && rbSelf != null)
        {
            Vector2 relativeVelocity = rbOther.velocity - rbSelf.velocity;

            Vector2 collisionNormal = collision.GetContact(0).normal;

            if (relativeVelocity.magnitude < 0.1f)
            {
                rbSelf.position += collisionNormal * 0.1f;
                rbOther.position -= collisionNormal * 0.1f;
            }
            else
            {
                Vector2 newVelocitySelf = Vector2.Reflect(rbSelf.velocity, collisionNormal);
                Vector2 newVelocityOther = Vector2.Reflect(rbOther.velocity, collisionNormal);

                rbSelf.velocity = newVelocitySelf;
                rbOther.velocity = newVelocityOther;

            }

            flipDirection();
        }
    }

}
