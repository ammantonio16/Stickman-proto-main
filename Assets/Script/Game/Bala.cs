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
        Destroy(this.gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Cuerda")
        {
            Destroy(cuerda);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Heracles");
            ZombieLife vidaEnemy = collision.gameObject.GetComponentInParent<ZombieLife>();
            //vidaEnemy.DañoRecibidoEnemy(daño);//
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Sombis"))
        {
            Debug.Log("Pere Martínez");
            ZombieLife vidaSombi = collision.GetComponentInParent<ZombieLife>();
            vidaSombi.DañoRecibidoZombie(daño);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Cuerda")
        {
            Destroy(this.gameObject);
            Destroy(cuerda);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ZombieLife vidaEnemy = collision.gameObject.GetComponentInParent<ZombieLife>();
            vidaEnemy.DañoRecibidoEnemy(daño);
            Destroy(this.gameObject);
        }
    }
}
