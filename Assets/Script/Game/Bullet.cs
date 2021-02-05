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
    }


    void Update()
    {
        rb.velocity = transform.right * speed;
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
        if (collision.tag == "Ground")
        {
            Destroy(this.gameObject);
            Turn.turnos = false;
        }
        if(collision.tag == "Enemy" || collision.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;
        }
    }
    #endregion
}
