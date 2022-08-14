using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovimientoGanchoporBoton : MonoBehaviour
{
    SpriteRenderer verde;
    public Transform gancho;
    public float speed;
    bool activar;
    bool izquierda;
    bool derecha;
    void Start()
    {
        verde = GetComponent<SpriteRenderer>();
        activar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activar)
        {
            MovimientoGancho();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BalaPlayer")
        {
            verde.color = new Color(0f, 255f, 0f, 255f);
            activar = true;
            derecha = true;
        }
    }
    void MovimientoGancho()
    {
        if (izquierda)
        {
            derecha = false;
            gancho.Translate(Vector2.right * -speed * Time.deltaTime);
            if(gancho.position.x <= -8.34)
            {
                derecha = true;
            }
        }
        if (derecha)
        {
            izquierda = false;
            gancho.Translate(Vector2.right * speed * Time.deltaTime);
            if (gancho.position.x >= 8.34)
            {
                izquierda = true;
            }
        }
    }
}
