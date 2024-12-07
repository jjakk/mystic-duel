using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField]private float lifespan;
    [SerializeField]private int worth;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(startCountdown());
    }

    // Update is called once per frame
    void Update() {
        // if (transform.position.y < -5) {
        //     Destroy(this.gameObject);
        // }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Player player = other.GetComponent<Player>();
            player.CollectCoin(worth);
            Destroy(this.gameObject);
        }
    }
    public IEnumerator startCountdown() {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
