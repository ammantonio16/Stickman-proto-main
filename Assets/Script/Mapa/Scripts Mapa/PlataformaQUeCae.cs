using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQUeCae : MonoBehaviour
{
    public HingeJoint2D plataformaCae;
    private void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {

            plataformaCae.enabled = false;

        }
    }
}
