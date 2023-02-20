using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;


    private Transform target;
    
    
    //to change with states
    private bool isChasing;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing == true)
        {
            
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
