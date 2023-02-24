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
    public AudioSource gameOver;
    public AudioSource victory;
    public AudioSource timeUp;

    private bool fadingMenu;
    private bool fadingGame;
    private bool fadingGO;
    private bool fadingVictory;
    private bool fadingTimeUp;

    [Range(0,1)] private float fadeGame;
    [Range(0,1)] private float fadeMenu;
    [Range(0,1)] private float fadeGO;
    [Range(0,1)] private float fadeVictory;
    [Range(0,1)] private float fadeTimeUp;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void FadeMenu(float target)
    {
        fadingMenu = true;
        fadeMenu = target;
    }
    
    public void FadeVictory(float target)
    {
        fadingVictory = true;
        fadeVictory = target;
    }
    public void FadeGO(float target)
    {
        fadingGO = true;
        fadeGO = target;
    }
    public void FadeTimeUp(float target)
    {
        fadingTimeUp = true;
        fadeTimeUp = target;
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
        if (fadingGO == true)
        {
            if(gameOver.volume == fadeGO)
            {
                fadingGO = false;
            }
            else
            {
                gameOver.volume = Mathf.Lerp(gameOver.volume, fadeGO, fadeDuration);
            }
        }
        if (fadingVictory == true)
        {
            if(victory.volume == fadeVictory)
            {
                fadingVictory = false;
            }
            else
            {
                victory.volume = Mathf.Lerp(victory.volume, fadeVictory, fadeDuration);
            }
        }
        if (fadingTimeUp == true)
        {
            if(timeUp.volume == fadeTimeUp)
            {
                fadingTimeUp = false;
            }
            else
            {
                timeUp.volume = Mathf.Lerp(timeUp.volume, fadeTimeUp, fadeDuration);
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
        
        if (victory.volume == 0)
        {
            victory.Stop();
            
        }
        else if (victory.isPlaying == false && victory.volume != 0)
        {
            victory.PlayOneShot(victory.clip);
        }
        
        if (gameOver.volume == 0)
        {
            gameOver.Stop();
        }
        else if (gameOver.isPlaying == false && gameOver.volume != 0)
        {
            gameOver.PlayOneShot(gameOver.clip);
        }
        
        if (timeUp.volume == 0)
        {
            timeUp.Stop();
        }
        else if (timeUp.isPlaying == false && timeUp.volume != 0)
        {
            timeUp.Play();
        }
    }
}
