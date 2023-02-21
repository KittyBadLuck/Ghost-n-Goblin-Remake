using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DeplacementTest : MonoBehaviour
{
    public float speed = 1f;
    public float speedLadder = 5f;
    public float jumpStrenght = 5f;
    public float speedMax = 10f;
    private Rigidbody rb;
    public Transform groundCheck;
    public LayerMask Ground;

    private float _horizontalInput;
    private float _verticalInput;
    private bool nearLadder;
    private bool isGrounded;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, Ground);
        MyInput();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
        if (nearLadder && _verticalInput != null)
        {
            Takeladder();
        }
    }

    private void MyInput()
    {
        //Get Horizontal and Vertical Axis
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.forward * _horizontalInput;
        rb.AddForce(moveDirection * speed, ForceMode.Force);
        if (rb.velocity.z > speedMax || rb.velocity.z < -speedMax)
        {
            rb.velocity = Vector3.Lerp(rb.velocity,new Vector3(rb.velocity.x, rb.velocity.y, moveDirection.z* speedMax) , 0.5f);
        }
        
        //flip player
        if(_horizontalInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = newRotation;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
    }

    private void Takeladder()
    {
        Vector3 moveDirection = Vector3.up* _verticalInput;
        rb.AddForce(moveDirection *speedLadder, ForceMode.Force);
        if (rb.velocity.y > speedMax || rb.velocity.y < -speedMax)
        {
            rb.velocity = Vector3.Lerp(rb.velocity,new Vector3(rb.velocity.x, moveDirection.y* speedMax, rb.velocity.z) , 0.5f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            nearLadder = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            nearLadder = false;
        }
    }
}
