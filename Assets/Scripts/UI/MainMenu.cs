using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindWithTag("Music").GetComponent<AudioManager>();
        _audioManager.FadeMenu(1);
    }

    public void PlayScene()
    {
        _audioManager.FadeGame(1);
        _audioManager.FadeMenu(0);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
