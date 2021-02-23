﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private float xBounds = 4.4f;
    private float yPos = -2.0f;
    private float minSpeed = 10.0f;
    private float maxSpeed = 15.0f;
    private float torqueValueRange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();


        transform.position = RandomPosition();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), ForceMode.Impulse);
    }

    private Vector3 RandomPosition()
    {
        float xPos = Random.Range(-xBounds, xBounds);
        return new Vector3(xPos, yPos);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private Vector3 RandomTorque()
    {
        float xValue = Random.Range(-torqueValueRange, torqueValueRange);
        float yValue = Random.Range(-torqueValueRange, torqueValueRange);
        float zValue = Random.Range(-torqueValueRange, torqueValueRange);
        return new Vector3(xValue, yValue, zValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}