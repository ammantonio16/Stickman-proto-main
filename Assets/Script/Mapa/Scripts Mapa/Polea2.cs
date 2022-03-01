using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea2 : MonoBehaviour
{

    public bool bajarPowerUp;
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
        if (bajarPowerUp)
        {
            caja1.transform.position = Vector2.MoveTowards(caja1.transform.position, puntoArribaIzquierda.position, step);
            caja2.transform.position = Vector2.MoveTowards(caja2.transform.position, puntoAbajoDerecha.position, step);
            if (caja1.transform.position == puntoArribaIzquierda.position && caja2.transform.position == puntoAbajoDerecha.position)
            {
                polea1.SetActive(true);
                polea2.SetActive(false);
                bajarPowerUp = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {
            bajarPowerUp = true;
        }
    }
}
