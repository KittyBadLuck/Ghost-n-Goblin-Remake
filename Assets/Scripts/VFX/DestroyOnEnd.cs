using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnd : MonoBehaviour
{
    public float Duration = 10f;
   private float timer = 0f;

   // Update is called once per frame
    void Update()
    {
        if (timer >= Duration)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
