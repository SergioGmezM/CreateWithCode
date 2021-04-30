using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Constants

    private float THRESHOLD = 0.0001f;

    // Public variables

    public float gravityModifier = 1.5f;
    public float jumpForce = 400.0f;
    public float speed = 500.0f;
    public float rotationSpeed = 5.0f;


    // Private variables

    private GameManager gameManager;
    private Rigidbody playerRB;
    private Animator playerAnim;

    private bool isJumping = false;

    private float horizontalMove;
    private float maxSqrtVelocity = 15.0f;
    private float xBounds = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnim = GetComponentInChildren<Animator>();
        Physics.gravity *= gravityModifier;
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            horizontalMove = Input.GetAxis("Horizontal");

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalMove < 0 ? -1.0f : 1.0f, 0.0f, 0.0f));

            //Rotate smoothly to this target:
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

            if (horizontalMove > -THRESHOLD && horizontalMove < THRESHOLD)
            {
                playerAnim.SetFloat("Speed_f", 0.0f);
            }

                if (Input.GetKeyDown(KeyCode.Space) && playerRB.velocity.y > -THRESHOLD && playerRB.velocity.y < THRESHOLD)
            {
                playerAnim.SetBool("Jump_b", true);
                isJumping = true;
            }

            ConstrainPlayerPosition();
        }
    }

    // FixedUpdate is called once per physics calculation
    private void FixedUpdate()
    {
        if (gameManager.isGameActive)
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
            playerAnim.SetBool("Jump_b", false);

            // Moves the player horizontally
            if (playerRB.velocity.sqrMagnitude < maxSqrtVelocity)
                playerRB.AddForce(Vector3.right * horizontalMove * speed * 0.5f);

        } else 
        {
            if (playerRB.velocity.sqrMagnitude < maxSqrtVelocity)
                playerRB.AddForce(Vector3.right * horizontalMove * speed);

            playerAnim.SetFloat("Speed_f", 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player falls off the platforms
        if (other.gameObject.CompareTag("Sensor"))
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When player lands on platform, it isn't jumping anymore
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
            // Takes the platform's transform as the objects parent
            transform.parent = collision.gameObject.transform;
        }

        // If player hits an enemy, substract health and destroy enemy
    }

    // When the player jumps off a platform, it's no longer its parent
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
