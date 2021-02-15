using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float startTime = 3.0f;
    public float spawnInterval = 4.0f;
    public float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", startTime, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObject()
    {
        
        Instantiate(objectToSpawn, generateSpawnPosition(), objectToSpawn.transform.rotation);
    }

    private Vector3 generateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 2.5f, spawnPosZ);
    }
}
