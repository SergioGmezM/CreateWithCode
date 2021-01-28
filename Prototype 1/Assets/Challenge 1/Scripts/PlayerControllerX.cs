using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    float ACCELERATION_THRESHOLD = 0.01f;

    public float speed = 20.0f;
    public float rotationSpeed = 100.0f;

    private float verticalInput;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int isAccelerating;
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");
        // get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput > ACCELERATION_THRESHOLD || horizontalInput < -ACCELERATION_THRESHOLD)
        {
            isAccelerating = 1;
        } else
        {
            isAccelerating = 0;
        }

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * horizontalInput * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.left, verticalInput * isAccelerating * rotationSpeed * Time.deltaTime);
    }
}
