using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionMenuVolumen2 : MonoBehaviour
{
    public GameObject[] boton;
    public GameObject[] posicionesBotones;
    public GameObject pantallaCampaign;
    public Animator maxYMin;
    public float count;
    public bool boo;
    public int prueba;
    public float numeroBoton;
    public float aparecerCanvas;
    public GameObject pantalla;
    public EntendimientoPestañas pa;
    public Canvas canvasPestaña;
    void Start()
    {
        boton = GameObject.FindGameObjectsWithTag("Botones");
        posicionesBotones = GameObject.FindGameObjectsWithTag("PosicionBarraTareas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MinimizarYMaximizar(int ident)
    {
        boo = !boo;
        if (!boo)
        {
            StopCoroutine("AparecerCanvas");
            canvasPestaña.SetActive(false);
            //EntendimientoPestañas.activarBotones4 = true;
            maxYMin.SetBool("Minimizar", true);
            maxYMin.SetBool("Maximizar", false);
            StartCoroutine("Aparecer");
            /*foreach (GameObject botonesPantalla in boton)
            {
                botonesPantalla.SetActive(true);
            }*/
            foreach (GameObject botonesPantalla in AlmacenBotones4.boton2)
            {
                botonesPantalla.SetActive(false);
            }
            for (int i = 0; i < AlmacenBotones4.boton2.Length; i++)
            {
                AlmacenBotones4.boton2[ident].SetActive(false);
            }
            for (int i = 0; i < AlmacenBotones4.botonCerrar.Length; i++)
            {
                AlmacenBotones4.botonCerrar[ident].SetActive(false);
            }

        }
        if (boo)
        {
            StartCoroutine("AparecerCanvas");
            StopCoroutine("Aparecer");
            EntendimientoPestañas.activarBotones4 = false;
            EntendimientoPestañas.eliminado[ident] = true;
            pantalla.SetActive(true);
            maxYMin.SetBool("Minimizar", false);
            maxYMin.SetBool("Maximizar", true);
            foreach (GameObject botonesPantalla in boton)
            {
                botonesPantalla.SetActive(false);
            }
            foreach (GameObject botonesPantalla in AlmacenBotones4.boton3)
            {
                if (botonesPantalla != null)
                {
                    botonesPantalla.SetActive(false);
                }
            }
            for (int i = 0; i < AlmacenBotones4.boton2.Length; i++)
            {
                AlmacenBotones4.boton2[ident].SetActive(true);
            }
            for (int i = 0; i < AlmacenBotones4.botonCerrar.Length; i++)
            {
                AlmacenBotones4.botonCerrar[ident].SetActive(true);
            }
        }
    }
    public void CerrarPestaña(int id)
    {
        StopCoroutine("AparecerCanvas");
        canvasPestaña.SetActive(false);
        boo = false;
        StartCoroutine("Aparecer");
        maxYMin.SetBool("Minimizar", true);
        maxYMin.SetBool("Maximizar", false);
        EntendimientoPestañas.eliminado[id] = false;
        foreach (GameObject botonesPantalla in AlmacenBotones4.boton2)
        {
            botonesPantalla.SetActive(false);
        }
        for (int i = 0; i < AlmacenBotones4.boton2.Length; i++)
        {
            AlmacenBotones4.boton2[id].SetActive(false);
        }
        for (int i = 0; i < AlmacenBotones4.botonCerrar.Length; i++)
        {
            AlmacenBotones4.botonCerrar[id].SetActive(false);
        }
    }
    public IEnumerator Aparecer()
    {
        yield return new WaitForSeconds(numeroBoton);
        foreach (GameObject botonesPantalla in boton)
        {
            botonesPantalla.SetActive(true);
            EntendimientoPestañas.activarBotones4 = true;
        }
    }
    public IEnumerator AparecerCanvas()
    {
        yield return new WaitForSeconds(aparecerCanvas);
        canvasPestaña.SetActive(true);
    }
}
