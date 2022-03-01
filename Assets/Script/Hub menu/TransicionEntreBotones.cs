using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TransicionEntreBotones : MonoBehaviour
{
    public GameObject[] boton;
    public GameObject[] boton2;
    public GameObject[] boton3;
    public GameObject[] posicionesBotones;
    public GameObject pantallaCampaign;
    public Animator maxYMin;
    public float count;
    public bool boo;
    public bool conteoPestaña;
    public int prueba;
    public float numeroBoton;
    public AlmacenBotones4 patata;
    

    void Start()
    {
        //Esto es una prueba de actualizar scripts
        boton = GameObject.FindGameObjectsWithTag("Botones");
        boton2 = GameObject.FindGameObjectsWithTag("Botones 2");
        boton3 = GameObject.FindGameObjectsWithTag("Botones 3");
        posicionesBotones = GameObject.FindGameObjectsWithTag("PosicionBarraTareas");
    }
    void Update()
    {
    }
    //El void Botones son las acciones que se van a realizar con los iconos del escritorio
    public void Botones(int identificador)
    {
        patata.pantallas[identificador].SetActive(true);
        //pantallaCampaign.SetActive(true);
        if (boton2[identificador].transform.position != posicionesBotones[identificador].transform.position)
        {
            boton2[identificador].transform.position = posicionesBotones[identificador].transform.position;
            MinimizarYMaximizar(identificador);
            patata.LogicaDeLasCapas(identificador);
            Destroy(boton3[identificador]);
            EntendimientoPestañas.eliminado[identificador] = true;
            if (EntendimientoPestañas.eliminado[identificador])
            {
                Debug.Log("Oye, has activado" +" "+ identificador);
            }
            
        }
        else
        {
            patata.LogicaDeLasCapas(identificador);
            MinimizarYMaximizar(identificador);
            AlmacenBotones4.LogicaPestañas(identificador);
            {
                for (int j = 0; j < boton2.Length; j++)
                {
                    if (j == identificador)
                    {
                        boton2[j].SetActive(true);
                    }
                    else
                    {
                        boton2[j].SetActive(false);
                    }

                }
            }
        }

        

    }
    //MinimizarYMaximizar se llamaran cuando acciones los iconos en la barra de tareas y el icono aparezca con un recuadro en blanco
    public void MinimizarYMaximizar(int ident)
    {
        boo = !boo;
        if (!boo)
        {
            NumeroPestañas.numeroTotalPestañas--;
            maxYMin.SetBool("Minimizar", true);
            maxYMin.SetBool("Maximizar", false);
            prueba = 0;
            if (NumeroPestañas.numeroTotalPestañas == 0)
            {
                AlmacenBotones4.LogicaPestañasDefault(ident);
                {
                    for (int j = 0; j < boton2.Length; j++)
                    {
                        boton2[ident].SetActive(false);
                    }
                }
            }
            if(NumeroPestañas.numeroTotalPestañas > 0)
            {
                patata.LogicaAlMinimizar(ident);
            }

        }
        if (boo)
        {
            patata.pantallas[ident].GetComponent<SpriteRenderer>().sortingOrder = patata.pantallas[ident].GetComponent<SpriteRenderer>().sortingOrder + AlmacenBotones4.sumaCapas;
            AlmacenBotones4.sumaCapas++;
            if(NumeroPestañas.numeroTotalPestañas <= NumeroPestañas.numeroLimitePestañas)
            {
                NumeroPestañas.numeroTotalPestañas++;
            }
            maxYMin.SetBool("Minimizar", false);
            maxYMin.SetBool("Maximizar", true);
            if (NumeroPestañas.numeroTotalPestañas > 1)
            {
                AlmacenBotones4.LogicaPestañas(ident);
                {
                    for (int j = 0; j < boton2.Length; j++)
                    {
                        if (j == ident)
                        {
                           
                            boton2[j].SetActive(true);
                        }
                        else
                        {
                            boton2[j].SetActive(false);
                        }

                    }
                }
            }
            if(NumeroPestañas.numeroTotalPestañas >= 1)
            {
                foreach (GameObject botonesPantalla in boton)
                {
                    botonesPantalla.SetActive(false);
                }
            }
        }
    }

    //OrdenDeCapas se llamara solo en los iconos de las barras de tareas con un rectángulo azul debajo; Superponionde las diferentes pestañas una encima de otra
    public void OrdenDeCapas(int id)
    {
        if (NumeroPestañas.numeroTotalPestañas <= 0)
        {
            boo = true;
            maxYMin.SetBool("Minimizar", false);
            maxYMin.SetBool("Maximizar", true);
            if (NumeroPestañas.numeroTotalPestañas <= NumeroPestañas.numeroLimitePestañas)
            {
                NumeroPestañas.numeroTotalPestañas++;
            }
        }
        else
        {
            boo = true;
            patata.LogicaDeLasCapas(id);
            maxYMin.SetBool("Minimizar", false);
            maxYMin.SetBool("Maximizar", true);
            if (NumeroPestañas.numeroTotalPestañas <= NumeroPestañas.numeroLimitePestañas)
            {
                NumeroPestañas.numeroTotalPestañas++;
            }
        }
        
        //patata.pantallas[id].GetComponent<SpriteRenderer>().sortingOrder = patata.pantallas[id].GetComponent<SpriteRenderer>().sortingOrder + AlmacenBotones4.sumaCapas;
        //AlmacenBotones4.sumaCapas++;
        AlmacenBotones4.LogicaPestañas(id);
        {
            for (int j = 0; j < boton2.Length; j++)
            {
                if (j == id)
                {
                    boton2[j].SetActive(true);

                }
                else
                {
                    boton2[j].SetActive(false);
                    
                }

            }
        }
    }
}

