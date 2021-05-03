using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    const float ACCELERATION_THRESHOLD = 0.01f;

    public GameObject centerOfMass;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI RPMText;
    public float horsePower = 50000;
    public const float turnSpeed = 35.0f;

    private Rigidbody vehicleRB;
    private float horizontalInput;
    private float forwardInput;
    private float speed;
    private float rpm;
    [SerializeField] private List<WheelCollider> wheels;

    // Start is called before the first frame update
    void Start()
    {
        vehicleRB = GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int isAccelerating;
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (vehicleOnGround())
        {
            if (forwardInput > ACCELERATION_THRESHOLD || forwardInput < -ACCELERATION_THRESHOLD)
            {
                isAccelerating = 1;
            }
            else
            {
                isAccelerating = 0;
            }

            // transform.Translate(Vector3.forward * speed * forwardInput * Time.deltaTime);
            vehicleRB.AddRelativeForce(Vector3.forward * horsePower * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime * isAccelerating);

            speed = Mathf.RoundToInt(vehicleRB.velocity.magnitude * 0.237f); // 3.6 for km/h
            speedText.SetText("Speed: " + speed + "m/s");

            rpm = Mathf.Round((speed % 30) * 40);
            RPMText.SetText("RPM: " + rpm);
        }            
    }

    bool vehicleOnGround()
    {
        int wheelsOnGround = 0;

        foreach(WheelCollider wheel in wheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        return wheelsOnGround == 4 ? true : false;
    }
}
