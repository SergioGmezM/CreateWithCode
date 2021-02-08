using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstacleObject;

    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private float minSpawnTime = 2.0f;
    private float maxSpawnTime = 8.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("InstantiateObject", 3.0f, Random.Range(minSpawnTime, maxSpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateObject()
    {
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstacleObject, spawnPos, obstacleObject.transform.rotation);
        }
    }
}
