using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharaController : MonoBehaviour
{
    public float speed = 1f;
    public float speedCrouch = 10f;
    public float speedLadder = 5f;
    public float jumpStrenght = 5f;
    public float speedMax = 10f;
    private Rigidbody rb;
    public Transform groundCheck;
    public LayerMask Ground;

    private float _horizontalInput;
    private float _verticalInput;
    private bool nearLadder;
    public bool isGrounded;
    private CapsuleCollider _collider;

    private Animator _animator;
    
    public int maxWeaponsLaunched =3;

    public int weaponInScene =0;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, Ground);
        MyInput();
        if (Input.GetButtonDown("Jump") && isGrounded && _animator.GetBool("IsThrowing") == false)
        {
            Jump();
        }
        if (Input.GetButtonUp("Fire1") && weaponInScene < maxWeaponsLaunched && Time.timeScale == 1)
        {
            _animator.SetBool("IsThrowing", true);
            _animator.SetBool("IsMoving", false);
        }

        if (rb.velocity.z == 0)
        {
            _animator.SetBool("IsMoving", false);
        }

        if (isGrounded)
        {
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsClimbing", false);
        }
        else if( _animator.GetBool("IsClimbing") == false)
        {
            _animator.SetBool("IsMoving", false);
            _animator.SetBool("IsJumping", true);
        }
    }

    private void FixedUpdate()
    {
        if ( _animator.GetBool("IsThrowing") == false)
        {
            Move();
        }

        if (nearLadder && _verticalInput != 0 && _animator.GetBool("IsThrowing") == false)
        {
            _animator.SetBool("IsMoving", false);
            Takeladder();
        }
        else if (nearLadder == false && _verticalInput < 0 && _animator.GetBool("IsThrowing") == false)
        {
            Crouch();
        }
        else
        {
            _animator.SetBool("IsClimbing", false);
            _animator.SetBool("IsCrouching", false);
            this.GetComponent<PlayerManager>().isCrouch = false;
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
        _animator.SetBool("IsMoving", true);
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
        _animator.SetBool("IsJumping", true);
        rb.AddForce((Vector3.up+ transform.forward) * jumpStrenght, ForceMode.Impulse);
    }

    private void Crouch()
    {
        _animator.SetBool("IsCrouching", true);
        this.GetComponent<PlayerManager>().isCrouch = true;
        speedMax = speedCrouch;
        _collider.center = new Vector3(0, -0.3f, 0);
       _collider.height = 1.5f;
        
    }
    private void Takeladder()
    {
        _animator.SetBool("IsClimbing", true);
        Vector3 moveDirection = Vector3.up* _verticalInput;
        rb.AddForce(moveDirection *speedLadder, ForceMode.Force);
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
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
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
        }
    }
}
