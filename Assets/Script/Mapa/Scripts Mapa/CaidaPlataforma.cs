using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaPlataforma : MonoBehaviour
{
    public HingeJoint2D resorteDerecha;
    public HingeJoint2D resorteIzquierda;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {
            resorteDerecha.enabled = false;
            resorteIzquierda.enabled = false;

        }
    }
}
