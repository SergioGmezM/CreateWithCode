using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float spawnRate = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawnTargetCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawnTargetCoroutine()
    {
        IEnumerator coroutine = SpawnTarget();
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
