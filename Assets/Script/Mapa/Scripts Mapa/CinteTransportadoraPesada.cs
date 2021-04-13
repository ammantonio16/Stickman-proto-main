using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinteTransportadoraPesada : MonoBehaviour
{
    Rigidbody2D rbCaja;
    public float velocidad;
    public bool z;
    void Start()
    {
        rbCaja = GetComponent<Rigidbody2D>();
        z = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (z)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cinta")
        {
            z = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CajasFuturo")
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CajasFuturo")
        {
            transform.parent = null;
        }
        if (collision.gameObject.tag == "Cinta")
        {
            rbCaja.constraints = RigidbodyConstraints2D.None;
        }
    }
}
