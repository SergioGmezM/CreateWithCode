using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlatform : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager platformSpawner;
    private float speed;
    private float xBounds = -22.0f;
    private bool movementStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        platformSpawner = GameObject.Find("Platform Spawner").GetComponent<SpawnManager>();
        movementStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            GameObject platformClone = GameObject.Find("Platform(Clone)");

            if (platformClone != null)
            {
                speed = platformClone.GetComponent<MoveLeft>().speed;
                float seconds = platformSpawner.timeInterval;

                if (!movementStarted)
                {
                    StartCoroutine(MoveLeft(seconds));
                } else
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }


                if (transform.position.x < xBounds)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator MoveLeft(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        movementStarted = true;
    }
}
