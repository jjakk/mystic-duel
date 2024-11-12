using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawning : MonoBehaviour {
    public Fireball fireball;
    [SerializeField]private float spawnDelay;
    // Start is called before the first frame update
    void Start() {
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
        Instantiate(fireball, new Vector3(Random.Range(-7.5f, 7.5f), 6, 0), Quaternion.identity);
        StartCoroutine(startSpawning());
    }
}
