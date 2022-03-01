using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NumeroPestañas : MonoBehaviour
{
    public static int numeroTotalPestañas;
    public static int numeroLimitePestañas;
    public int iconos;
    public float numeroBoton;
    public GameObject[] boton;
    public GameObject[] boton4;
    void Start()
    {
        boton = GameObject.FindGameObjectsWithTag("Botones");
        numeroLimitePestañas = iconos;
    }

    // Update is called once per frame
    void Update()
    {
        
        /*Debug.Log(numeroTotalPestañas.ToString());
        if (numeroTotalPestañas == 0)
        {
            StartCoroutine("Desparecer");
            StopCoroutine("Aparecer");
        }
        else
        {
            StartCoroutine("Aparecer");
            StopCoroutine("Desparecer");
        }*/
    }
    /*public IEnumerator Desparecer()
    {
        yield return new WaitForSeconds(numeroBoton);
        foreach (GameObject botonesPantalla in boton)
        {
            botonesPantalla.SetActive(true);
        }
    }
    public IEnumerator Aparecer()
    {
        yield return new WaitForSeconds(numeroBoton);
        foreach (GameObject botonesPantalla in boton)
        {
            botonesPantalla.SetActive(false);
        }
    }*/
}
