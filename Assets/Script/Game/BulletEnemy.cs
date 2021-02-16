using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [Header("Características de la bala Enemy")]
    Rigidbody2D rb;
    public float speed;
    public float daño = 0;
    public int cantidad;

    [Header("Dirección de la bala Enemy")]
    public Transform rangoBala1;
    public Transform rangoBala2;
    public Transform rangoBala3;
    public Vector2 rangobala;
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
        transform.position = Vector2.MoveTowards(transform.position, rangobala, speed * Time.deltaTime);
    }

    //Trigger que detecta las colisiones de la balaEnemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Life>().VidaBaja(10);
            Destroy(this.gameObject);
            Turn.turnos = true;
        }
        if (collision.tag == "Ground")
        {
            Destroy(this.gameObject);
            Turn.turnos = true;
        }
        if (collision.tag == "Player" || collision.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;

        }
    }
}
