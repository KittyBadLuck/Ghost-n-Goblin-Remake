using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float damage =3;

    public float reculStrenght =2;
    public Vector3 recul;

    public float lifeMax =10;

    private float life =0;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life >= lifeMax)
        {
            _animator.SetBool("Die", true);
        }
        else
        {
            life += Time.deltaTime;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            recul = new Vector3(0, 0, collision.transform.position.z - transform.position.z).normalized;
            recul *= reculStrenght;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, recul);
        }
    }
}
