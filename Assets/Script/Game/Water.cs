using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float velocidad = 10f;
    
    private void Update()
    {
        transform.Translate(new Vector3(1f, 0f, 0f) * velocidad * Time.deltaTime);

        if(transform.position.x > 10)
        {
            velocidad = -velocidad;
        }

        if(transform.position.x < -10)
        {
            velocidad = -velocidad;
        }
    }
}
