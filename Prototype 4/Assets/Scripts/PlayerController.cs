using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody playerRB;
    private GameObject focalPoint;
    private float maxSqrtVelocity = 10.0f;
    private float forwardInput = 0.0f;
    private float previousForwardInput;
    private bool changeDirection = false;

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
        if (playerRB.velocity.sqrMagnitude < maxSqrtVelocity)
        {
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
        }

        if (changeDirection)
        {
            playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput * 400.0f);
            changeDirection = false;
        }
    }
}
