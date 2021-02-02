using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    public float daño = 0;

    public int cantidad;

    public ContadordeTiempo ct;
    public ContadordeTiempo cte;

    Enemy nb;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        rb.velocity = transform.right * speed;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Life>().VidaBaja(10);
            Destroy(this.gameObject);
            Turn.turnos = false;
            /*Destroy(gameObject);
            Destroy(collision.gameObject);*/


        }   
        if (collision.tag == "Ground")
        {
            Destroy(this.gameObject);
            Turn.turnos = false;
        }
        if(collision.tag == "Enemy" || collision.tag == "Ground")
        {
            ct.enabled = false;
            cte.tiempoAcabarTurno = 20f;
            cte.enabled = true;

        }
    }

    //prueba de daño al enemigo
    /*private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            currentDamageTime += Time.deltaTime;
            playerVida.vida += -20f;
            print ("estamos");
            
        }
    }*/
    
}
