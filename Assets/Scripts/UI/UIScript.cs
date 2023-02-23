using System;
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

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
           highScore =  PlayerPrefs.GetInt("HighScore");
           highText.text = "HighScore : " + highScore;
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }
       
    }

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
           Continue();
        }
        
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score : " + score;
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
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
