using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    public float daño = 0;

    public int cantidad;

    public ContadordeTiempo cte;
    public ContadordeTiempo ct;

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
        if (collision.tag == "Player")
        {
            collision.GetComponent<Life>().VidaBaja(10);
            Destroy(this.gameObject);
            Turn.turnos = true;
            /*Destroy(gameObject);
            Destroy(collision.gameObject);*/


        }
        if (collision.tag == "Ground")
        {
            Destroy(this.gameObject);
            Turn.turnos = true;
        }
        if (collision.tag == "Player" || collision.tag == "Ground")
        {
            cte.enabled = false;
            ct.tiempoAcabarTurno = 20f;
            ct.enabled = true;

        }
    }
}
