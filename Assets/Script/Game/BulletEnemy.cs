using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [Header("Características de la bala Enemy")]
    Rigidbody2D rb;
    public float speed;
    public float daño;
    
    
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            LifePlayer vidaPlayer = collision.GetComponentInParent<LifePlayer>();
            vidaPlayer.VidaBaja(daño);
            //GameObject enemigoLife = GameObject.FindGameObjectWithTag("Player");
            //PlayerLife.life.health -= daño;
        }
        if (collision.gameObject.CompareTag("Sombis"))
        {
            Destroy(this.gameObject);
            ZombieLife vidaSombi = collision.GetComponentInParent<ZombieLife>();
            vidaSombi.DañoRecibidoZombie(daño);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);

        }
    }

}
