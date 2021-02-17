using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public float startTime = 3.0f;
    public float spawnInterval = 4.0f;
    public float spawnRange = 9.0f;

    private int enemyCount;
    private int waveNumber = 3;
    private int maxWaveNumber = 20;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            if (waveNumber < maxWaveNumber)
                waveNumber++;

            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
            enemyCount = waveNumber;
        }
    }

    private void SpawnEnemyWave(int nEnemies)
    {
        for (int i = 0; i < nEnemies; i++)
        {
            Instantiate(enemyPrefab, generateSpawnPosition(2.5f), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, generateSpawnPosition(0.2f), powerupPrefab.transform.rotation);
    }

    private Vector3 generateSpawnPosition(float yPos = 0.0f)
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, yPos, spawnPosZ);
    }
}
