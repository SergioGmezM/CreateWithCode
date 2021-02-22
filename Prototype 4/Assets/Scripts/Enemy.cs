using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;

    private Transform playerTransform;
    private Rigidbody enemyRB;
    private float maxSqrtVelocity = 9.0f;
    private float yBounds = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -yBounds)
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
