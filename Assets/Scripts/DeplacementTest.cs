using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementTest : MonoBehaviour
{
    public float speed = 1f;
    public float speedMax = 10f;
    private Rigidbody rb;

    private float _horizontalInput;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void MyInput()
    {
        //Get Horizontal and Vertical Axis
        _horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        Vector3 moveDirection = this.transform.forward * _horizontalInput;
        rb.AddForce(moveDirection * speed, ForceMode.Force);
        if (rb.velocity.magnitude > speedMax)
        {
            rb.velocity = moveDirection * speedMax;
        }
    }
}
