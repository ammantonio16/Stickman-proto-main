using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarril : MonoBehaviour
{
    
    public GameObject explosionPrefab;

    public float rangeExpolosionDamage;
    public float daño;
    public Collider2D barrilCollider;
    public LayerMask layer;

    void Start()
    {
    }

    
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "BalaPlayer")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject ExplosionEffect = Instantiate(explosionPrefab, barrilCollider.bounds.center, Quaternion.identity);
            Destroy(ExplosionEffect, 5f);
            if (rangeExpolosionDamage > 0)
            {
                Collider2D[] dañoExplosionRango;
                dañoExplosionRango = Physics2D.OverlapCircleAll(barrilCollider.bounds.center, rangeExpolosionDamage);
                 
                foreach(Collider2D obj in dañoExplosionRango)
                {
                    Debug.Log("!");
                    if (obj.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("Macaco");
                        obj.gameObject.GetComponentInParent<Life2Enemy>().VidaBaja(daño);
                    }
                    if (obj.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("Macaco");
                        obj.gameObject.GetComponentInParent<Life2Enemy>().VidaBaja(daño);
                    }
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject ExplosionEffect = Instantiate(explosionPrefab, barrilCollider.bounds.center, Quaternion.identity);
            Destroy(ExplosionEffect, 5f);
            if (rangeExpolosionDamage > 0)
            {
                Collider2D[] dañoExplosionRango;
                dañoExplosionRango = Physics2D.OverlapCircleAll(barrilCollider.bounds.center, rangeExpolosionDamage);
                
                foreach (Collider2D obj in dañoExplosionRango)
                {
                    Debug.Log("!");
                    if (obj.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("Macaco");
                        obj.gameObject.GetComponentInParent<Life2Enemy>().VidaBaja(daño);
                    }
                    if (obj.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Macaco");
                        obj.gameObject.GetComponentInParent<Life2Enemy>().VidaBaja(daño);
                    }
                }
            }

        }
    }
}

