using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestriy : MonoBehaviour
{  private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
}
