using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBala : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float daño;
    float tiempoExplosion;
    public GameObject explosionPrefab;
    public float rangeExpolosionDamage;
    public LayerMask layer;
    GameObject player;
    GameObject enemy;
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

    // Update is called once per frame
    void Update()
    {
        tiempoExplosion += Time.deltaTime; 
        if (tiempoExplosion >= 5)
        {
            Destroy(gameObject);
            GameObject ExplosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(ExplosionEffect, 5f);
            if (rangeExpolosionDamage > 0)
            {
                Collider2D dañoExplosionRango;
                dañoExplosionRango = Physics2D.OverlapCircle(transform.position, rangeExpolosionDamage, layer);
                if (dañoExplosionRango)
                {
                    player.GetComponent<Life>().VidaBaja(daño);
                    enemy.GetComponent<Life>().VidaBaja(daño);
                }
            }
        }
    }
}
