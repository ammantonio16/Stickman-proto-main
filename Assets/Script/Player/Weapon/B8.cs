using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B8 : MonoBehaviour
{
    Rigidbody2D rb;
    B8Arma b8;

    float timeTag;
    public float timeTagLimit;
    public float speed;

    bool b8tag;

    int onceOnly;
    bool derecha;
    void Start()
    {
        b8 = FindObjectOfType<B8Arma>();
        b8tag = true;

        rb = GetComponent<Rigidbody2D>();
        if (JugadorMovimiento.direccionBala)
        {
            rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
            derecha = true;
        }
        if (!JugadorMovimiento.direccionBala)
        {
            rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
            derecha = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //This is mode that Player not collision when throw B8
        if (b8tag)
        {
            timeTag += Time.deltaTime;
            if (timeTag >= timeTagLimit) 
            {
                this.gameObject.tag = "B8";
                b8tag = false;
            }
            
        }
        //Debug.Log("B8" + " " + rb.velocity.magnitude);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Faker")) 
        {
            b8.municionTotal++;
            Destroy(this.gameObject);
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("B8 al golpear" + " " + rb.velocity.magnitude);
            SoldierLife vidaEnemy = collision.gameObject.GetComponentInParent<SoldierLife>();
            if(rb.velocity.magnitude >= 7)
            {
                vidaEnemy.Daño(5);
                rb.velocity = Vector2.zero;
            }
        
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (rb.velocity.magnitude >= 10 && onceOnly < 1)
            {
                if (derecha)
                {
                    //rb.velocity = Vector2.right * 10;
                }
                else
                {
                    //rb.velocity = -Vector2.right * 10;
                }
                onceOnly++;
            }
        }
    }
}
