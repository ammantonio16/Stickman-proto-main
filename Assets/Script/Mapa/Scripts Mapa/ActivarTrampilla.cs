using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarTrampilla : MonoBehaviour
{
    public HingeJoint2D resorteDerecha;
    public HingeJoint2D resorteIzquierda;
    public Rigidbody2D rbDerecha;
    public Rigidbody2D rbIzquierda;
    SpriteRenderer verde;
    private void Start()
    {
        verde = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {
            verde.color = new Color(0f, 255f, 0f, 255f);
            rbDerecha.constraints = RigidbodyConstraints2D.None;
            rbIzquierda.constraints = RigidbodyConstraints2D.None;
            resorteDerecha.enabled = true;
            resorteIzquierda.enabled = true;

        }
    }
}
