using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [Header("Características de la bala Enemy")]
    Rigidbody2D rb;
    public float speed;
    public float daño;
    public int cantidad;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Turn.direccionbalaEnemy)
        {
            rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        }
        if (!Turn.direccionbalaEnemy)
        {
            rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
       
    }

    //Trigger que detecta las colisiones de la balaEnemy
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Life>().VidaBaja(10);
            Destroy(this.gameObject);
            Turn.turnos = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            //Destroy(this.gameObject);
            Turn.turnos = true;
        }
        if (collision.gameObject.tag == "Player" || collision.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;

        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {          
            Destroy(this.gameObject);
            GameObject playerLife = GameObject.FindGameObjectWithTag("Player");
            playerLife.GetComponent<Life>().VidaBaja(daño);
            
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
            
        }

        
    }
}
