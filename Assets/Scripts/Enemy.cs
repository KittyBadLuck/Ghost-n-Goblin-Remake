using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;


    private Transform target;
    private Rigidbody rb;
    
    
    //to change with states
    private bool isChasing;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing == true)
        {
            Vector3 moveDirection = (target.position - this.transform.position).normalized;
            rb.AddForce(moveDirection * speed * Time.deltaTime, ForceMode.Force);
        }
    }
    

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isChasing = true;
        }
    }
}
