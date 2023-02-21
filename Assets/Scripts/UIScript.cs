using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public int highScore;

    public int score;

    public TextMeshProUGUI highText;

    public TextMeshProUGUI scoreText;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && isPaused == false)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Cancel") && isPaused == true)
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
        
    }

    public void UpdateScore()
    {
        scoreText.text = "Score : " + score;
        if (score >= highScore)
        {
            highText.text = "HighScore : " + highScore;
        }
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
