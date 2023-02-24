using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public int highScore;

    public int score;

    public float maxTimer = 120;
    private float totalTime;
    private int minutes;
    private int seconds;

    public TextMeshProUGUI highText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public GameObject victoryScreen;
    public TextMeshProUGUI remainTime;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScoreText;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private bool isPaused;
    private bool goPlayed;
    private bool victoryPlayed;

    private AudioManager _audioManager;

    private void Awake()
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

        totalTime = maxTimer;
       _audioManager = GameObject.FindWithTag("Music").GetComponent<AudioManager>();
        
        victoryPlayed = false;
        goPlayed = false;

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
        totalTime -= Time.deltaTime;
        UpdateLevelTimer(totalTime );

        if (totalTime <= 20)
        {
            _audioManager.FadeGame(0.2f);
            _audioManager.FadeTimeUp(1);
        }

        if (totalTime <= 0)
        {
            _audioManager.FadeTimeUp(0);
            GameOver();
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
        SceneManager.LoadScene(0);
        _audioManager.FadeGame(0);
        _audioManager.FadeVictory(0);
        _audioManager.FadeGO(0);
        _audioManager.FadeMenu(1);
        _audioManager.FadeTimeUp(0);

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
        _audioManager.FadeTimeUp(0);
        _audioManager.FadeGame(0.2f);
        _audioManager.FadeGO(1);
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void Victory()
    {
        _audioManager.FadeTimeUp(0);
        _audioManager.FadeGame(0.2f);
        _audioManager.FadeVictory(1);
        victoryScreen.SetActive(true);
        Time.timeScale = 0;
        remainTime.text = timerText.text;
        int final = score + (seconds + (minutes * 60));
        finalScore.text = final.ToString();
        if (final > highScore)
        {
            highScore = final;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        else
        {
            highScoreText.text = highScore.ToString();

        }
    }

    public void Retry()
    {
        _audioManager.FadeTimeUp(0);
        _audioManager.FadeVictory(0);
        _audioManager.FadeGO(0);
        _audioManager.FadeGame(1);
        
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


    public void UpdateLevelTimer(float totalSeconds)
    {
         minutes = Mathf.FloorToInt(totalSeconds / 60f);
         seconds = Mathf.RoundToInt(totalSeconds % 60f);
 
        string formatedSeconds = seconds.ToString();
 
        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
 
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
