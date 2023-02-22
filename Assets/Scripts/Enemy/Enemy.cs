using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;
    public float speedMax = 10f;
    public float distanceAttack = 5f;
    public float attackRange = 5f;
    public bool distanceChase;

    [Space]
    //random Move 
    private float timerWaitMove = 0;
    private float timerMove = 0;
    public float WaitTimeForMove = 5f;
    public float RandomMoveDuration = 5f;
    private float RandomDirection;
    
    [Space]
    [Header("Stat")] 
    public float attackRate = 3f;
    public float healthMax = 10f;
    public float currentHealth = 10f;
    public int givenScore;

    private float timerAttack;
    
    //other ref
    private Transform target;
    private Rigidbody rb;
    private Vector3 distance;
    private UIScript _uiScript;
    private EnemyLoot _enemyLoot;


    //to change with states
    private bool isChasing;
    private bool isAttacking;
    private bool isRandomMove;
    public bool isHit;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       
        _enemyLoot = GetComponentInParent<EnemyLoot>();
        target = GameObject.FindWithTag("Player").transform;
        _uiScript = GameObject.FindWithTag("MainMenu").GetComponent<UIScript>();
        rb = this.GetComponent<Rigidbody>();
        timerAttack = attackRate;
        currentHealth = healthMax;
        timerWaitMove = WaitTimeForMove;
        RandomDirection = Random.Range(-1f, 1f);
       // animator = GetComponent<Animator>();
       animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
        distance = target.position - this.transform.position;
        if (isHit == false)
        {
            if (distance.magnitude < distanceAttack)
            {
                rb.velocity = Vector3.zero;
                isChasing = false;
                isAttacking = true;
            
            }
            else if (distanceChase && distance.magnitude > distanceAttack)
            {
                isChasing = true;
                isAttacking = false;
                animator.SetBool("IsAttacking", false);
            }
            else if (distanceChase == false)
            {
                isChasing = false;
                timerAttack = attackRate;
                animator.SetBool("IsAttacking", false);
            }
        }

        if (isAttacking == true)
        {
            if (timerAttack >= attackRate)
            {
                animator.SetBool("IsAttacking", true);
               // Attack();
                timerAttack = 0;
            }
            else
            {
                timerAttack += Time.deltaTime;
            }

        }

        if (distanceChase == false && isHit == false) 
        {
            if (timerWaitMove >= WaitTimeForMove)
            {
                isRandomMove = true;
                if (timerMove >= RandomMoveDuration)
                {
                    RandomDirection = Random.Range(-1f, 1f);
                    timerWaitMove = 0;
                    timerMove = 0;
                    isRandomMove = false;

                }
                else
                {
                    timerMove += Time.deltaTime;
                }
            }
            else
            {
                timerWaitMove += Time.deltaTime;
            }

        }
    }
    
  

    private void FixedUpdate()
    {
        
        if (isHit == false)
        {
           
            if (isChasing == true)
            {
                Debug.Log("walk");
                Vector3 moveDirection = new Vector3(0, 0, distance.z).normalized;
                rb.AddForce(moveDirection * speed, ForceMode.Force); 
                if (rb.velocity.magnitude > speedMax)
                {
                    rb.velocity = moveDirection * speedMax;
                }

                transform.forward = moveDirection;
                Debug.Log(rb.velocity);
            }

            if (isRandomMove)
            {
                Debug.Log("walk");
                Vector3 moveDirection = new Vector3(0, 0, RandomDirection).normalized;
                rb.AddForce(moveDirection * speed, ForceMode.Force); 
                if (rb.velocity.magnitude > speedMax)
                {
                    rb.velocity = moveDirection * speedMax;
                }
                transform.forward = moveDirection;
            }
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            distanceChase = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            distanceChase = false;
        }
    }

    public void Attack()
    {
        if (distance.magnitude <= attackRange)
        {
            target.GetComponent<PlayerManager>().TakeDamage();
        }
    }

    public void TakeDamage(float damage, Vector3 recul)
    {
        isHit = true;
        currentHealth -= damage;
        rb.velocity = Vector3.zero;
        rb.AddForce(recul, ForceMode.Impulse);
       animator.SetBool("IsHit", true);

        if (currentHealth <= 0)
        {
            animator.SetBool("IsHit", false);
            animator.SetBool("IsDying", true);
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
