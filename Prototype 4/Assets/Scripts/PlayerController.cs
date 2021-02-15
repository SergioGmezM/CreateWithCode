using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody playerRB;
    private GameObject focalPoint;
    private float powerUpStrength = 15.0f;
    private float maxSqrtVelocity = 10.0f;
    private float forwardInput = 0.0f;
    private float previousForwardInput;
    private bool changeDirection = false;
    private bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Camera Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        previousForwardInput = forwardInput;
        forwardInput = Input.GetAxis("Vertical");

        if (Mathf.Sign(previousForwardInput) != Mathf.Sign(forwardInput))
        {
            changeDirection = true;
        }
    }

    private void FixedUpdate()
    {
        // Keeps the player from going too fast
        if (playerRB.velocity.sqrMagnitude < maxSqrtVelocity)
        {
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
        }

        // If a change in direction has been made, forces the player to remove the inertia faster
        if (changeDirection)
        {
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput * 400.0f);
            changeDirection = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = collision.gameObject.transform.position - transform.position;
            enemyRB.AddForce(knockbackDirection * powerUpStrength, ForceMode.Impulse);
        }
    }
}
