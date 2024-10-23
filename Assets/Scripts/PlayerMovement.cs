using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    protected Rigidbody2D rigidBody;
    enum Direction { Left, Right };
    private Direction direction;
    public int moveSpeed;

    // Start is called before the first frame update
    void Start() {
        direction = Direction.Right;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.RightArrow)) {
            direction = Direction.Right;
            rigidBody.velocity = new Vector2(moveSpeed, 0);
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            direction = Direction.Left;
            rigidBody.velocity = new Vector2(-1 * moveSpeed, 0);
        }
    }
}
