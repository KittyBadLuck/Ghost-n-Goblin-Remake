using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Sprite sprite;
    public float speed = 5f;
    public float damage = 2f;
    public float recul = 1f;

    private float maxLife = 10f;
    private float life = 0;

    public PlayerAttack playerattackScript;
    
    //Different type of physics
    public bool arcThrow; //to see if it throwing in an arc or straight
    public bool blockAttack; //to see if it is blocking attacks
    public bool createFire; //to see if it is creating fire on ground
    public GameObject Fire;
    
    //add destroys
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //hit enemy 
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, recul);
            Destroy(gameObject);
            playerattackScript.weaponInScene--;
        }
        else if(createFire && collision.gameObject.CompareTag("Ground"))
        {
            GameObject fire = Instantiate(Fire, collision.GetContact(0).point, Quaternion.identity);
            //destroy if only it wall
            Destroy(gameObject);
             playerattackScript.weaponInScene--;
        }
        else
        {
            Destroy(gameObject);
            playerattackScript.weaponInScene--;
        }
    }

    private void Update()
    {
        //destroy if it never collides with smtg
        life += Time.deltaTime;
        if (life >= maxLife)
        {
            Destroy(gameObject);
            playerattackScript.weaponInScene--;
        }
    }
}
