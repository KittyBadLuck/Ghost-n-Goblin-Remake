using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public bool isWeapon;
    public bool givenScore;

    public GameObject projectile; //if weapon, get the projectile to instantiate

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isWeapon)
            {
                
            }
        }
    }
}
