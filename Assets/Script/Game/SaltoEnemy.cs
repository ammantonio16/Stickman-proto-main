using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaltoEnemy : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    public float vc;
    



    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            enemyRb.AddForce(Vector2.up * vc, ForceMode2D.Impulse);
        }
    }
}
