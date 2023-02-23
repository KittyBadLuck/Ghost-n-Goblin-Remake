using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Range(0,1)]public float volume = 1f;
    [Range(0,1)]public float fadeDuration = 0.5f;


    //music list
    public AudioSource gameMusic;
    public AudioSource MenuMusic;

    private bool fadingMenu;
    private bool fadingGame;

    [Range(0,1)] private float fadeGame;
    [Range(0,1)] private float fadeMenu;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void FadeMenu(float target)
    {

        fadingMenu = true;
        fadeMenu = target;
    }

    public void FadeGame(float target)
    {
        fadingGame = true;
        fadeGame = target;

    }

    private void Update()
    {
        if (fadingGame == true)
        {
            if(gameMusic.volume == fadeGame)
            {
                fadingGame = false;
            }
            else
            {
                gameMusic.volume = Mathf.Lerp(gameMusic.volume, fadeGame, fadeDuration);
            }
        }

        if (fadingMenu == true)
        {
            if(MenuMusic.volume == fadeMenu)
            {
                fadingMenu = false;
            }
            else
            {
                MenuMusic.volume = Mathf.Lerp(MenuMusic.volume, fadeMenu, fadeDuration);
            }
        }
        
    }

    private void LateUpdate()
    {
        if (gameMusic.volume == 0)
        {
            gameMusic.Stop();
        }
        else if (gameMusic.isPlaying == false  && gameMusic.volume != 0)
        {
            gameMusic.Play();
        }
        if (MenuMusic.volume == 0)
        {
            MenuMusic.Stop();
        }
        else if (MenuMusic.isPlaying == false && MenuMusic.volume != 0)
        {
            MenuMusic.Play();
        }
    }
}
