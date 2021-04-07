using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitadorDeObjetos : MonoBehaviour
{
    public int numerodeCajas;
    public int numeroLimitedeCajas;
    public GameObject contenedorSpawns;
    public GameObject contenedorSpawns1;
    public GameObject contenedorSpawns2;
    void Start()
    {
        
    }
    private void Update()
    {
        if(numerodeCajas >= numeroLimitedeCajas)
        {
            contenedorSpawns.SetActive(false);
            contenedorSpawns1.SetActive(false);
            contenedorSpawns2.SetActive(false);
        }
        if(numerodeCajas < numeroLimitedeCajas)
        {
            contenedorSpawns.SetActive(true);
            contenedorSpawns1.SetActive(true);
            contenedorSpawns2.SetActive(true);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CajasFuturo")
        {
            numerodeCajas++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CajasFuturo")
        {
            numerodeCajas--;
        }
    }
}
