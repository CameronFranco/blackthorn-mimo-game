using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public SpriteRenderer sprite;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    private bool groundCollision;
    private bool doubleJumped;
    public CanvasGroup canvasYouWin;
    private int lives = 3;

    void Start()
    {

    }

    void FixedUpdate()
    {
        groundCollision = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Update()
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        var transform = GetComponent<Transform>();

        if (lives == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKey("right"))
        {
            sprite.flipX = false;
            if (Input.GetKey("left shift"))
            {
                rigidBody.velocity = new Vector2(10, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(5, rigidBody.velocity.y);
            }
        }

        if (Input.GetKey("left"))
        {
            sprite.flipX = true;
            rigidBody.velocity = new Vector2(-5, rigidBody.velocity.y);
        }

        if (groundCollision)
        {
            doubleJumped = false;
        }

        if (Input.GetKeyDown("space") && groundCollision)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 10);
        }

        if (Input.GetKeyDown("space") && !groundCollision && !doubleJumped)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 10);
            doubleJumped = true;
        }

        if (transform.position.y < -6)
        {
            if (transform.position.x < 2)
            {
                transform.position = new Vector2(-5, 2);
            }
            else
            {
                transform.position = new Vector2(2, 2);
            }
            lives--;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "EnemyDamage")
        {
            lives--;
            transform.position = new Vector2(2, 1);           
        }
        else if (other.name == "Exit")
        {
            canvasYouWin.alpha = 1;
        }
    }
}

