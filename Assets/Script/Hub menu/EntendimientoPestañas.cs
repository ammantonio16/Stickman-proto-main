using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntendimientoPestañas : MonoBehaviour
{
    public static GameObject[] transiciones;
    public static bool[] eliminado;
    public static bool activarBotones4;
    public float aparecerBotonesTiempo;
    void Start()
    {
        //Esto es una prueba
        transiciones = GameObject.FindGameObjectsWithTag("Transixion");
        Debug.Log(transiciones.Length);
        for (int i = 0; i <= transiciones.Length; i++)
        {
            eliminado = new bool[i];

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activarBotones4)
        {
            for (int i = 0; i < eliminado.Length; i++)
            {
                if (eliminado[i])
                {
                    AlmacenBotones4.boton4[i].SetActive(true);
                    AlmacenBotones4.boton3[i].SetActive(false);
                }
                else
                {
                    AlmacenBotones4.boton4[i].SetActive(false);
                    AlmacenBotones4.boton3[i].SetActive(true);
                }
            }
        }
        if (!activarBotones4)
        {
            for (int w = 0; w < transiciones.Length; w++)
            {
                if (eliminado[w])
                {
                    AlmacenBotones4.boton4[w].SetActive(false);
                }
               
            }
        }
    }
}
