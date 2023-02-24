using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> weaponInventory = new List<GameObject>();
    public Image weaponPannel;
    private int currentWeaponInt = 0;
    public int damageTaken = 0;
    public bool isCrouch;
    


    //ref to other script
    public PlayerAnimation attackScript;
    public Animator _animatorArmor;
    private UIScript uiScript;
    private Animator _animator;
   

    private void Awake()
    {
        attackScript.currentWeapon = weaponInventory[0];
        weaponPannel.sprite = weaponInventory[0].GetComponent<WeaponBehaviour>().sprite;
        uiScript = GameObject.FindWithTag("MainMenu").GetComponent<UIScript>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            currentWeaponInt +=(int) Input.GetAxis("Mouse ScrollWheel");
            if (currentWeaponInt < 0)
            {
               currentWeaponInt = weaponInventory.Count -1;
            }
            else if (currentWeaponInt > weaponInventory.Count -1)
            {
                currentWeaponInt = 0;
            }
            
            weaponPannel.sprite = weaponInventory[currentWeaponInt].GetComponent<WeaponBehaviour>().sprite;
            attackScript.currentWeapon = weaponInventory[currentWeaponInt];
        }
    }

    public void TakeDamage()
    {

        if (isCrouch == false)
        {
            if (damageTaken == 0)
            {
                _animator.SetBool("IsHit", true);
            }
            else if (damageTaken >= 1)
            {
                
               
                _animator.SetBool("IsDead", true);
            }
             
        }
        
    }

    public void Die()
    {
        uiScript.GameOver();
        Destroy(gameObject);
    }

    public void GetArmor()
    {
        _animatorArmor.SetBool("Desintegrate", false);
        _animatorArmor.SetBool("Appear", true);
        
    }
}
