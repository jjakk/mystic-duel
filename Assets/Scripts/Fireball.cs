using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    [SerializeField]private float lifespan;
    [SerializeField]private int damage;
    [SerializeField]private Player player;
    [SerializeField] private GameObject smoke;
    public Animator animator;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(startCountdown());
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        // if (transform.position.y < -5) {
        //     Destroy(this.gameObject);
        // }
    }
    void OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        if(other.tag == "Player"){
            other.GetComponent<Player>().takeDamage(damage);
            
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;

            Explode();
            //Destroy(this.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            Player player = GetComponentInParent<Player>();

            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;

            Explode();
        }
    }

    private void Explode()
    {
        smoke.SetActive(false);
        
        animator.SetTrigger("Explode");

        Destroy(gameObject, 0.3f);
    }

    public IEnumerator startCountdown() {
        yield return new WaitForSeconds(lifespan);
        GameManager.incrementScore();
        Destroy(this.gameObject);
    }
}
