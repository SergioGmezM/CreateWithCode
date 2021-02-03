using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnX = 15.0f;
    private float spawnZ = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            int indexPrefab = Random.Range(0, animalPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnX, spawnX), 0, spawnZ);
            Instantiate(animalPrefabs[indexPrefab], spawnPos, animalPrefabs[indexPrefab].transform.rotation);
        }        
    }
}
