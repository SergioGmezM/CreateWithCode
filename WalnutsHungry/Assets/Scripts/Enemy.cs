using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;

    private Transform playerTransform;
    private Rigidbody enemyRB;
    private float maxSqrtVelocity = 4.0f;
    private float yBounds = 12.0f;
    private float xBounds = 23.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > xBounds || transform.position.x < -xBounds ||
            transform.position.y > yBounds || transform.position.y < -yBounds)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (enemyRB.velocity.sqrMagnitude < maxSqrtVelocity)
        {
            Vector3 lookDirection = (playerTransform.position - enemyRB.position).normalized;
            enemyRB.AddForce(lookDirection * speed);
        }
    }
}
