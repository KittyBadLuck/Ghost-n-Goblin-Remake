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
    public float reculStrenght = 1f;
    private Vector3 recul;

    private float maxLife = 10f;
    private float life = 0;

    public PlayerAttack playerattackScript;
    public GameObject explosion;
    
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
            recul = new Vector3(0, 0, collision.transform.position.z - transform.position.z).normalized;
            recul *= reculStrenght;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, recul);
        }
        else if(createFire && collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(Fire, collision.GetContact(0).point + new Vector3(0,0.1f,0), Quaternion.identity);
        }
        Impact();
    }

    private void Impact()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        playerattackScript.weaponInScene--;
        Destroy(gameObject);
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
