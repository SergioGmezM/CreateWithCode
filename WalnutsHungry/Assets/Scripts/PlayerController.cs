using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Constants

    private float THRESHOLD = 0.0001f;

    // Public variables

    public bool gameOver = false;

    public float gravityModifier = 1.5f;
    public float jumpForce = 400.0f;
    public float speed = 500.0f; 


    // Private variables

    private Rigidbody playerRB;

    private bool isJumping = false;

    private float horizontalMove;
    private float maxSqrtVelocity = 15.0f;
    private float xBounds = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            horizontalMove = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && playerRB.velocity.y > -THRESHOLD && playerRB.velocity.y < THRESHOLD)
            {
                isJumping = true;
            }

            ConstrainPlayerPosition();
        }
    }

    // FixedUpdate is called once per physics calculation
    private void FixedUpdate()
    {
        if (!gameOver)
        {
            MovePlayer();
        }
    }

    // Constrains the player's movement
    void ConstrainPlayerPosition()
    {
        if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, 0);
        }
        else if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, 0);
        }
    }

    // Moves the player's position
    void MovePlayer()
    {
        if (isJumping)
        {
            // Makes the player jump
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;

            // Moves the player horizontally
            if (playerRB.velocity.sqrMagnitude < maxSqrtVelocity)
                playerRB.AddForce(Vector3.right * horizontalMove * speed * 0.5f);

        } else 
        {
            if (playerRB.velocity.sqrMagnitude < maxSqrtVelocity)
                playerRB.AddForce(Vector3.right * horizontalMove * speed);
        }
    }
  
    private void OnCollisionEnter(Collision collision)
    {
        // When player lands on platform, it isn't jumping anymore
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
        }

        // If player hits an enemy, substract health and destroy enemy

        // If player hits a heart, add health and destroy heart
    }
}
