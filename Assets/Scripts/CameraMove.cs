using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        offset = this.transform.position - target.position;
    }

    private void FixedUpdate()
    {
        this.transform.position = target.position + offset;
    }
}
