using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [Header("Características disparo")]
    public float speed;
    public float range;
    public Transform arrowPosition;
    Rigidbody2D rb;
    bool drag = true;
    Vector3 dis;
    SpriteRenderer sprite;

    [Header("Spawn del bitmap")]
    public GameObject arrow;
    public Transform spawn;

    [Header("Características hereditarias")]
    public GameObject objetoPadre;
    public GameObject objetoHijo;

    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        sprite = GetComponent<SpriteRenderer>();
        /*sprite.enabled = false;*/
    }

    //Detecta cuando el jugador arrastra su dedo por la pantalla
    void OnMouseDrag()
    {
        /*sprite.enabled = false;*/
        if (!drag)
            return;
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dis = pos - arrowPosition.position;
        dis.z = 0;

        if (dis.magnitude > range)
        {
            dis.z = 0;
            dis = dis.normalized * range;
        }
        transform.position = dis + arrowPosition.position;
    }
    //Detecta cuando el jugador suelta el dedo de la pantalla
    void OnMouseUp()
    {
        /*sprite.enabled = true; */
        if (!drag)
            return;
        drag = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = -dis.normalized * speed * dis.magnitude * speed / range;
        
    }
    //Trigger que detecta con que colisiona el bitmap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            objetoHijo = Instantiate(arrow, transform.position, transform.rotation);
            objetoHijo.transform.parent = objetoPadre.transform;
            objetoHijo.transform.position = objetoPadre.transform.position;
            //Destroy(this.gameObject);
            //Turn.turnos = false;
        }

        if (collision.gameObject.tag == "Enemy"){
            collision.GetComponent<Life>().VidaBaja(10);
            Destroy(this.gameObject);
            Turn.turnos = false;
        }

        if (collision.tag == "Enemy" || collision.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;
        }
    }
}
