using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujeBala : MonoBehaviour
{
    [Header("Características de la bala Player")]
    Rigidbody2D rb;
    public float speed;
    GameObject cuerda;
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
        cuerda = GameObject.FindGameObjectWithTag("Cuerda");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Turn.turnos = false;
            PowerUpBala.powerUpActiVerde = false;
        }
        if (collision.gameObject.tag == "Ground")
        {
            //Destroy(this.gameObject);
            //Turn.turnos = false;
            PowerUpBala.powerUpActiVerde = false;

        }
    }
}
