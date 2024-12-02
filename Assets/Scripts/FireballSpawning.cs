using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawning : MonoBehaviour {
    public Fireball fireball;
    public Potion powerup;
    public Coin coin;
    public GameObject playerObj;
    private Player player;
    [SerializeField]private float spawnDelay;
    [SerializeField]private float fireballGravityScale;
    [SerializeField]private float spawnDelayAcceleration;
    [SerializeField]private float fireballGravityScaleAcceleration;
    // Start is called before the first frame update
    void Start() {
        fireball.GetComponent<Rigidbody2D>().gravityScale = fireballGravityScale;
        StartCoroutine(startSpawning());
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    IEnumerator startSpawning(){
        yield return new WaitForSeconds(spawnDelay);
        spawnFireball();
    }
    void spawnFireball() {
        int genCode = ((int) Random.Range(0, 100));
        bool genPowerup = genCode == 0;
        bool genCoin = genCode == 1;
        int streak = player.getStreak();
        if(streak > 0){
            genCoin = genCoin || genCode == 2;
        }
        if(streak > 1) {
            genCoin = genCoin || genCode == 3;
        }
        if(streak > 2) {
            genCoin = genCoin || genCode == 4;
        }


        if(genPowerup) {
            Instantiate(powerup, new Vector3(Random.Range(-7.5f, 7.5f), 6, 0), Quaternion.identity);
        }
        else if(genCoin) {
            Instantiate(coin, new Vector3(Random.Range(-7.5f, 7.5f), 6, 0), Quaternion.identity);
        }
        else {
            Instantiate(fireball, new Vector3(Random.Range(-7.5f, 7.5f), 6, 0), Quaternion.identity);
            fireball.GetComponent<Rigidbody2D>().gravityScale += fireballGravityScaleAcceleration;
            if(spawnDelay > 0.1) {
                spawnDelay -= spawnDelayAcceleration;
            }
        }
        StartCoroutine(startSpawning());
    }
}
