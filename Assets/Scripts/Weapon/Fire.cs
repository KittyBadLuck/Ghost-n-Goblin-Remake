using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float damage =3;

    public float recul =2;

    public float lifeMax =10;

    private float life =0;

    // Update is called once per frame
    void Update()
    {
        if (life >= lifeMax)
        {
            Destroy(gameObject);
        }
        else
        {
            life += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, recul);
        }
    }
}
