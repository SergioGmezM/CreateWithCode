using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;

    public float topBound;
    public float bottomBound;
    public float zPos = 0.0f;
    public float timeInterval;

    private GameManager gameManager;
    private float xPos = 22.0f;
    private IEnumerator coroutine;
    private bool nextSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive && nextSpawn)
        {
            nextSpawn = false;
            startSpawnCoroutine();
        } else if (!gameManager.isGameActive)
        {
            stopSpawnCoroutine();
        }
    }

    IEnumerator spawnCoroutine()
    {
        yield return new WaitForSeconds(timeInterval);
        SpawnObject();
        nextSpawn = true;
    }

    private void startSpawnCoroutine()
    {
        coroutine = spawnCoroutine();
        StartCoroutine(coroutine);
    }

    private void stopSpawnCoroutine()
    {
        coroutine = spawnCoroutine();
        StopCoroutine(coroutine);
    }

    private Vector3 GetRandomLocation(float topBound, float bottomBound)
    {
        float yPos = Random.Range(bottomBound, topBound);
        return new Vector3(xPos, yPos, zPos);
    }

    private void SpawnObject()
    {
        Vector3 spawnPos = GetRandomLocation(topBound, bottomBound);
        Instantiate(objectToSpawn, spawnPos, objectToSpawn.transform.rotation);
    }
}
