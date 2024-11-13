using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawning : MonoBehaviour {
    public Fireball fireball;
    public Fireball powerup;
    [SerializeField]private float spawnDelay;
    [SerializeField]private float fireballGravityScale;
    [SerializeField]private float spawnDelayAcceleration;
    [SerializeField]private float fireballGravityScaleAcceleration;
    // Start is called before the first frame update
    void Start() {
        fireball.GetComponent<Rigidbody2D>().gravityScale = fireballGravityScale;
        StartCoroutine(startSpawning());
    }

    // Update is called once per frame
    void Update() {
        
    }
    IEnumerator startSpawning(){
        yield return new WaitForSeconds(spawnDelay);
        spawnFireball();
    }
    void spawnFireball() {
        bool genPowerup = ((int) Random.Range(0, 50)) == 7;
        if(genPowerup) {
            Instantiate(powerup, new Vector3(Random.Range(-7.5f, 7.5f), 6, 0), Quaternion.identity);
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
