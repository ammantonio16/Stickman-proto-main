using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caer_Cabeza : MonoBehaviour
{
    Rigidbody2D rb;

    public int indexModificar;

    [SerializeField] Caida_Objeto caidaObjeto;
    [SerializeField] JugadorMovimiento playerAltura;

    int vecesSaltadas;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaPlayer"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Ground")) 
        {
            //Cuando choca contra el suelo tambien deberia modificarse
            StatusGameobjectsVariables.statusGameobject.modificacion[indexModificar].modificacion = true;
            caidaObjeto.ObjetoInGround();
        }
        if (collision.gameObject.CompareTag("RangoEnemy"))
        {
            StatusGameobjectsVariables.statusGameobject.modificacion[indexModificar].modificacion = true;
            caidaObjeto.ObjetoInGround();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vecesSaltadas++;
            if(vecesSaltadas >= 3)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1;
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Cuando choca contra el suelo tambien deberia modificarse
            StatusGameobjectsVariables.statusGameobject.modificacion[indexModificar].modificacion = true;
            caidaObjeto.ObjetoInGround();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Mathf.Abs(playerAltura.diferenciaAltura) > 5)
            {
                Debug.Log("FD");
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(0f, -1f),ForceMode2D.Impulse);
            }
        }
    }
}
