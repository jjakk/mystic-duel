using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    enum Direction { Left, Right };
    private Direction direction;

    // Start is called before the first frame update
    void Start() {
        direction = Direction.Right;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.RightArrow)) {
            direction = Direction.Right;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            direction = Direction.Left;
        }
    }
}
