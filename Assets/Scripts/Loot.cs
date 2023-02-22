using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public bool isWeapon;
    public int givenScore;

    public GameObject projectile; //if weapon, get the projectile to instantiate
    private PlayerManager _playerManager;
    private UIScript _uiScript;

    private void Start()
    {
        _uiScript = GameObject.FindWithTag("MainMenu").GetComponent<UIScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWeapon == true)
            {
                _playerManager = other.GetComponent<PlayerManager>();
                CheckPlayerWeapon();
                Destroy(gameObject);
            }
            else 
            {
                AddScore();
                Destroy(gameObject);
            }
        }
    }

    private void CheckPlayerWeapon()
    {
        bool haveThisWeapon = false;
        foreach (GameObject playerWeapon in _playerManager.weaponInventory)
        {
            if (projectile == playerWeapon)
            {
                haveThisWeapon = true;
            }
        }

        if (haveThisWeapon == true)
        {
            AddScore();
            Destroy(gameObject);
        }
        else
        {
            _playerManager.weaponInventory.Add(projectile);
            Destroy(gameObject);
        }
    }

    private void AddScore()
    {
        _uiScript.score += givenScore;
        _uiScript.UpdateScore();
    }
}
