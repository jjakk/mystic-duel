using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    protected Rigidbody2D rigidBody;
    enum Direction { Idle, Left, Right };
    private Direction direction;
    public int moveSpeed;
    public int maxHealth;
    private int health;
    // private int highScore;
    public Image healthBar;

    // Start is called before the first frame update
    void Start() {
        this.reset();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            this.flipDirection();
        }

        if(direction == Direction.Right) {
            rigidBody.velocity = new Vector2(moveSpeed, 0);
        }
        else if(direction == Direction.Left) {
            rigidBody.velocity = new Vector2(-1 * moveSpeed, 0);
        }
    }
    public void reset() {
        GameManager.resetScore();
        health = maxHealth;
        direction = Direction.Idle;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void takeDamage(int damage) {
        health -= damage;
        healthBar.fillAmount = ((float)health / maxHealth);
    }

    public int getHealth() {
        return health;
    }

    public void flipDirection() {
        if(direction == Direction.Right) {
            direction = Direction.Left;
        }
        else {
            direction = Direction.Right;
        }
    }
}
