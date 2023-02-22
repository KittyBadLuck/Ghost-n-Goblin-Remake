using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;
    public float speedMax = 10f;
    public float distanceAttack = 5f;
    public float attackRange = 5f;
    public float distanceChase = 20f;
    [Space] 
    [Header("Stat")] 
    public float attackRate = 3f;
    public float healthMax = 10f;
    public float currentHealth = 10f;
    public int givenScore;

    private float timer;


    private Transform target;
    private Rigidbody rb;
    private Vector3 distance;
    private UIScript _uiScript;
    private EnemyLoot _enemyLoot;
    
    
    //to change with states
    private bool isChasing;
    private bool isAttacking;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        _uiScript = GameObject.FindWithTag("MainMenu").GetComponent<UIScript>();
        _enemyLoot = this.GetComponent<EnemyLoot>();
        rb = this.GetComponent<Rigidbody>();
        timer = attackRate;
        currentHealth = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        distance = target.position - this.transform.position;
        if (distance.magnitude < distanceAttack)
        {
            rb.velocity = Vector3.zero;
            isChasing = false;
            isAttacking = true;
        }
        else if (distance.magnitude < distanceChase && distance.magnitude > distanceAttack)
        {
            isChasing = true;
            isAttacking = false;
        }
        else if (distance.magnitude > distanceChase)
        {
            isChasing = false;
            timer = attackRate;
        }


        if (isAttacking == true)
        {
            if (timer >= attackRate)
            {
                Attack();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isChasing == true)
        {
            Vector3 moveDirection = new Vector3(0, 0, distance.z).normalized;
            rb.AddForce(moveDirection * speed, ForceMode.Force);
            if (rb.velocity.magnitude > speedMax)
            {
                rb.velocity = moveDirection * speedMax;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        if (distance.magnitude <= attackRange)
        {
            target.GetComponent<PlayerManager>().TakeDamage();
        }
    }

    public void TakeDamage(float damage, float recul)
    {
        currentHealth -= damage;
        rb.AddForce(-this.transform.forward*recul, ForceMode.Impulse);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _enemyLoot.Loot(transform.position);
        _uiScript.score += givenScore;
        _uiScript.UpdateScore();
        Destroy(gameObject);
    }


}
