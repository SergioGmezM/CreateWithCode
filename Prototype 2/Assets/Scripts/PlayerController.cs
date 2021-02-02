using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public float xBounds = 15.0f;
    public GameObject projectilePrefab;

    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        } else if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
