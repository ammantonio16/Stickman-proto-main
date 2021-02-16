using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Características de la bala Player")]
    Rigidbody2D rb;
    public float speed;
    public float daño = 0;
    public int cantidad;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Turn.direccionbala)
        {
            rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        }
        if (!Turn.direccionbala)
        {
            rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
        }

    }


    void Update()
    {
        
    }
    //Trigger que detecta las colisiones de la balaPlayer
    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Life>().VidaBaja(10);
            Destroy(this.gameObject);
            Turn.turnos = false;
        }
        /*if (collision.tag == "Ground")
        {
            Destroy(this.gameObject);
            Turn.turnos = false;
        }*/
        /*if(collision.tag == "Enemy" || collision.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;
        }*/
    }
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Turn.turnos = false;
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
            Turn.turnos = false;
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;
        }
    }
}
