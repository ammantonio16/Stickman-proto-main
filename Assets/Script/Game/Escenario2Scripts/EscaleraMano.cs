using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraMano : MonoBehaviour
{
    Rigidbody2D rb;
    bool zonaEscalada;
    bool escalar;
    float vertical;
    public float velocidadSubir;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (zonaEscalada && Mathf.Abs(vertical) > 0f)
        {
            escalar = true;
        }
    }
    private void FixedUpdate()
    {
        if (escalar)
        {
            Debug.Log("Estoy Escalando");
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidadSubir);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EscalerasMano")
        {
            zonaEscalada = true;
            Debug.Log("Has entrado en zona de escalada");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EscalerasMano")
        {
            zonaEscalada = false;
            escalar = false;
        }
    }
}
