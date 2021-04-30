using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public bool isGameActive = false;

    private int score;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    }

    public void UpdateHealth(int healthToAdd)
    {
        health += healthToAdd;

        if (health <= 0)
        {
            health = 0;
            GameOver();
        } else if (health > 100)
        {
            health = 100;
        }

        healthText.text = "HEALTH: " + health;
    }

    public void StartGame()
    {
        score = 0;
        isGameActive = true;

        UpdateScore(0);
        UpdateHealth(100);

        titleScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
