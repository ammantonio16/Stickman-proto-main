using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class CintaTransportadoraLigero : MonoBehaviour
{
    Rigidbody2D rbCaja;
    public float velocidad;
    public bool z;
    public float speed;
    void Start()
    {
        rbCaja = GetComponent<Rigidbody2D>(); 
        z = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        speed = rbCaja.velocity.magnitude;
        if (speed >= 1)
        {
            speed = 1;
        }
        if (z)
        {
            rbCaja.AddForce(Vector2.right * velocidad * Time.deltaTime);

            //transform.Translate(Vector2.right * velocidad * Time.deltaTime);
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
