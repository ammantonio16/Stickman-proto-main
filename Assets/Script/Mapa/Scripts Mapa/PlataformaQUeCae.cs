using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQUeCae : MonoBehaviour
{
    public HingeJoint2D plataformaCae;
    SpriteRenderer cuerda;
    public Sprite cuerdaRota;
    private void Start()
    {
        cuerda = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {

            plataformaCae.enabled = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {

            plataformaCae.enabled = false;
            cuerda.sprite = cuerdaRota;

        }
    }

}
