using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollectible : MonoBehaviour
{
    private GameManager gameManager;

    public int scorePoints = 10;

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
            gameManager.UpdateScore(scorePoints);

            // Add some explosion effect?
            Destroy(gameObject);
        }
    }
}
