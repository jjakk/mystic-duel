using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawning : MonoBehaviour {
    public Fireball fireball;
    // Start is called before the first frame update
    void Start() {
        Instantiate(fireball, new Vector3(-5, 5, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
