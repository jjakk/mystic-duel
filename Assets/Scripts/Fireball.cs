using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    [SerializeField]private float lifespan;
    [SerializeField]private int damage;
    [SerializeField]private Player player;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(startCountdown());
    }

    // Update is called once per frame
    void Update() {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "player"){
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
