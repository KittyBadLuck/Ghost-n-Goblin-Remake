using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMorph : MonoBehaviour
{
    [SerializeField] private MeshFilter mesh1;
    [SerializeField] private Mesh mesh2;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mesh1.mesh = mesh2; 
        }
    }
}
