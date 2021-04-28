using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed = 8.0f;

    private GameManager gameManager;
    private float xBounds = -22.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (transform.position.x < xBounds)
            {
                Destroy(gameObject);
            }
        }
    }
}
