using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea1 : MonoBehaviour
{
    
    public bool conseguirPowerUp;
    public float velocidad;
    public Transform puntoArribaDerecha;
    public Transform puntoAbajoDerecha;
    public Transform puntoArribaIzquierda;
    public Transform puntoAbajoIzquierda;
    public GameObject caja1;
    public GameObject caja2;
    public GameObject polea1;
    public GameObject polea2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = velocidad * Time.deltaTime;
        if (conseguirPowerUp)
        {
            caja1.transform.position = Vector2.MoveTowards(caja1.transform.position, puntoAbajoIzquierda.position, step);
            caja2.transform.position = Vector2.MoveTowards(caja2.transform.position, puntoArribaDerecha.position, step);
            if(caja1.transform.position == puntoAbajoIzquierda.position && caja2.transform.position == puntoArribaDerecha.position)
            {
                polea1.SetActive(false);
                polea2.SetActive(true);
                conseguirPowerUp = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BalaPlayer")
        {
            conseguirPowerUp = true;
        }
    }
}
