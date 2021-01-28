using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float ACCELERATION_THRESHOLD = 0.01f;

    public float speed = 15.0f;
    public float turnSpeed = 35.0f;

    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int isAccelerating;
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if(forwardInput > ACCELERATION_THRESHOLD || forwardInput < -ACCELERATION_THRESHOLD)
        {
            isAccelerating = 1;
        } else
        {
            isAccelerating = 0;
        }

        transform.Translate(Vector3.forward * speed * forwardInput * Time.deltaTime);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime * isAccelerating);
    }
}
