using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;
    private float nextSpawnTime = 0.0f;
    private float minSpawnTime = 1.0f;
    private float maxSpawnTime = 5.0f;
    private bool isSpawning = false;

    // Initializes the timer for the next ball
    void setTimer()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        setTimer();
    }

    private void Update()
    {
        nextSpawnTime -= Time.deltaTime;

        if (nextSpawnTime <= 0.0f)
        {
            isSpawning = false;
        }

        if (!isSpawning)
        {
            isSpawning = true;
            SpawnRandomBall();
            setTimer();
        }
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        int ballIndex = Random.Range(0, 3);
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
    }

}
