using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Victory : MonoBehaviour
{

    private UIScript _uiScript;
    // Start is called before the first frame update
    void Awake()
    {
        _uiScript = GameObject.FindWithTag("MainMenu").GetComponent<UIScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiScript.Victory();
        }
    }
}
