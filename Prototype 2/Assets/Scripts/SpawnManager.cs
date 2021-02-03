using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    private float spawnX = 15.0f;
    private float spawnZ = 20.0f;
    private float startingTime = 2.0f;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startingTime, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {     
    }

    void SpawnRandomAnimal()
    {
        int indexPrefab = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnX, spawnX), 0, spawnZ);
        Instantiate(animalPrefabs[indexPrefab], spawnPos, animalPrefabs[indexPrefab].transform.rotation);
    }
}
