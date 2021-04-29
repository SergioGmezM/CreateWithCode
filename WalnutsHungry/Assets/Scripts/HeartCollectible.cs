using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    private GameManager gameManager;

    public int healthPoints = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        // Detects left-click
        if (Input.GetMouseButtonDown(0))
        {
            gameManager.UpdateHealth(healthPoints);

            // Add some explosion effect?
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player hits a heart, add health and destroy heart
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateHealth(healthPoints);

            // Add some explosion effect?
            Destroy(gameObject);
        }
    }
}
