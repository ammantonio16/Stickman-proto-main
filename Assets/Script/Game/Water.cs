using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float velocidad = 10f;
    public Transform position1;
    public Transform position2;
    
    private void Update()
    {
        transform.Translate(new Vector3(1f, 0f, 0f) * velocidad * Time.deltaTime);

        if(transform.position.x > position1.position.x)
        {
            velocidad = -velocidad;
        }

        if(transform.position.x < position2.position.x)
        {
            velocidad = -velocidad;
        }
    }
}
