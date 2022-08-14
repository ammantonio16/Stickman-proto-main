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

    }

    void Update()
    {
        Destroy(this.gameObject, 2f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoldierLife vidaEnemy = collision.gameObject.GetComponentInParent<SoldierLife>();
            vidaEnemy.Daño(1);
            Destroy(this.gameObject);
            ArmaController.preCollisionObject = false;


        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
            ArmaController.preCollisionObject = false;
        }
    }

   
}
