using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [Header("Características de la bala Player")]
    Rigidbody2D rb;
    public float speed;
    public float daño = 10;
    GameObject cuerda;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (JugadorMovimiento.direccionBala)
        {
            rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        }
        if (!JugadorMovimiento.direccionBala)
        {
            rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
        }
        cuerda = GameObject.FindGameObjectWithTag("Cuerda");

    }


    void Update()
    {
        
    }
    //Trigger que detecta las colisiones de la balaPlayer
    #region
    /*private void OnTriggerEnter2D(Collider2D collision)
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
        }*/
    
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            GameObject enemigoLife = GameObject.FindGameObjectWithTag("Enemy");
            enemigoLife.GetComponent<Life>().VidaBaja(daño);

        }
        if (collision.gameObject.tag == "Cuerda")
        {
            Destroy(cuerda);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cuerda")
        {
            Destroy(this.gameObject);
            Destroy(cuerda);
        }
    }
}
