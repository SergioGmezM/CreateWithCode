using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPoint;
    private float resetDistance;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        resetDistance = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < (startPoint.x - resetDistance))
        {
            transform.position = startPoint;
        }
    }
}
