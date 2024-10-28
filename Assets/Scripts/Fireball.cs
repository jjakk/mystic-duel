using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    [SerializeField]private float lifespan;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(startCountdown());
    }

    // Update is called once per frame
    void Update() {
        
    }
    public IEnumerator startCountdown() {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
