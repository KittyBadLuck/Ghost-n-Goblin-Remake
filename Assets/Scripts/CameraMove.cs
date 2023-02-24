using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;

    public Vector3 offset = new Vector3(15,0,0);
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
       // offset = this.transform.position - target.position;
    }

    private void FixedUpdate()
    {
        this.transform.position = target.position + offset;
    }
}
