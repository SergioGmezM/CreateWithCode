using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float DISTANCETHRESHOLD = 1.5f;

    public float speed = 30.0f;
    public int damage = 10;
    public float rotationSpeed = 5.0f;

    private GameManager gameManager;
    private Transform playerTransform;
    private Rigidbody enemyRB;
    private float maxSqrtVelocity = 30.0f;
    private float yBounds = 12.0f;
    private float xBounds = 23.0f;
    private bool chasePlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRB = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > xBounds || transform.position.x < -xBounds ||
            transform.position.y > yBounds || transform.position.y < -yBounds)
        {
            Destroy(gameObject);
        }

        if (chasePlayer && Vector3.Distance(playerTransform.position, enemyRB.position) < DISTANCETHRESHOLD)
        {
            DamagePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            if (enemyRB.velocity.sqrMagnitude <= maxSqrtVelocity)
            {
                Vector3 lookDirection = (playerTransform.position - enemyRB.position).normalized;

                // Gets away from the player
                if (!chasePlayer)
                {
                    lookDirection *= -1;
                }

                enemyRB.AddForce(lookDirection * speed);

                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                //Rotate smoothly to this target:
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
            }
            else
            {
                enemyRB.velocity /= 2;
            }
        } else
        {
            enemyRB.velocity = Vector3.zero;
        }
    }

    public void OnMouseOver()
    {
        // Detects right-click
        if (Input.GetMouseButtonDown(1))
        {
            // Add some explosion effect?
            Destroy(gameObject);
        } 
    }

    public void DamagePlayer()
    {
        gameManager.UpdateHealth(-damage);
        chasePlayer = false;
    }
}
