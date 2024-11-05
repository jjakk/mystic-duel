using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    [SerializeField]private float lifespan;
    [SerializeField]private int damage;
    [SerializeField]private Player player;
    public static int score = 0;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(startCountdown());
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < -5) {
            score++;
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.tag);
        if(other.tag == "Player"){
            other.GetComponent<Player>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
    public IEnumerator startCountdown() {
        yield return new WaitForSeconds(lifespan);
        player.incrementScore();
        Destroy(this.gameObject);
    }
}
