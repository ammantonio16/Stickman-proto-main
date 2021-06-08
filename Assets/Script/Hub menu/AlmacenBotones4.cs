using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AlmacenBotones4 : MonoBehaviour
{
    public static GameObject[] boton4;
    public static GameObject[] boton2;
    public static GameObject[] boton3;
    public static GameObject[] botonCerrar;
    public  GameObject[] pantallas;
    public  List<GameObject> pantallas2;
    public  int numPantallas2;
    public  int indiceComparativo;
    public int barbaracle;
    public TransicionEntreBotones boton;
    public float tiempoParaSaberElReseteo;
    public bool maracas;

    public static int sumaCapas = 1;
    // Start is called before the first frame update
    void Start()
    {
        boton3 = GameObject.FindGameObjectsWithTag("Botones 3");
        boton2 = GameObject.FindGameObjectsWithTag("Botones 2");
        for (int i = 0; i < boton2.Length; i++)
        {
            boton2[i].SetActive(false);
        }
        botonCerrar = GameObject.FindGameObjectsWithTag("Boton Cerrar");
        for (int i = 0; i < botonCerrar.Length; i++)
        {
            botonCerrar[i].SetActive(false);
        }
        pantallas = GameObject.FindGameObjectsWithTag("Pantalla");
        for (int w = 0; w < pantallas.Length; w++)
        {
            pantallas[w].SetActive(false);
        }
        boton4 = GameObject.FindGameObjectsWithTag("Botones 4");
        for (int i = 0; i < boton4.Length; i++)
        {
            boton4[i].SetActive(false);
        }
        indiceComparativo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LogicaAlMinimizar(int parata)
    {
        tiempoParaSaberElReseteo += Time.deltaTime;
        for (int i = 0; i < pantallas.Length; i++)
        {
            if(pantallas[parata].GetComponent<SpriteRenderer>().sortingOrder >= pantallas[i].GetComponent<SpriteRenderer>().sortingOrder)
            {
                Debug.Log("Soy Mayor");
            }
            if(pantallas[i].GetComponent<SpriteRenderer>().sortingOrder < pantallas[parata].GetComponent<SpriteRenderer>().sortingOrder)
            {
                numPantallas2++;
                pantallas2.Add(pantallas[i]);
                Debug.Log("Esta es mi lista actual" + " " + pantallas2.Count);
               
                for(int w = 0; w < pantallas2.Count; w++)
                {
                    indiceComparativo = 0;
                    if(pantallas2[w].GetComponent<SpriteRenderer>().sortingOrder > pantallas2[indiceComparativo].GetComponent<SpriteRenderer>().sortingOrder)
                    {
                        Debug.Log("Mis pestañas son " + " " +NumeroPestañas.numeroTotalPestañas);
                        Debug.Log("Soy la pantalla:" + " " + pantallas2[w]);
                        Debug.Log("Mi ID" + " " +pantallas2[w].GetComponent<IDBotones>().iD);
                        indiceComparativo++;
                        for (int r = 0; r < boton4.Length; r++)
                        {

                            if (r == pantallas2[w].GetComponent<IDBotones>().iD)
                            {
                                boton4[r].SetActive(false);
                            }
                            else
                            {
                                boton4[r].SetActive(true);
                            }
                        }
                        for (int j = 0; j < boton.boton2.Length; j++)
                        {
                            if (j == pantallas2[w].GetComponent<IDBotones>().iD)
                            {
                                tiempoParaSaberElReseteo += Time.deltaTime;
                                boton.boton2[j].SetActive(true);

                            }
                            else
                            {
                                boton.boton2[j].SetActive(false);
                                StartCoroutine("TiempoDeEsperaParaBorrarLaListaYResetearLosValores");

                            }

                        }
                    }
                    if(NumeroPestañas.numeroTotalPestañas == 1)
                    {
                        for (int r = 0; r < boton4.Length; r++)
                        {

                            if (r == pantallas2[w].GetComponent<IDBotones>().iD)
                            {
                                boton4[r].SetActive(false);
                            }
                            else
                            {
                                boton4[r].SetActive(true);
                            }
                            for (int j = 0; j < boton.boton2.Length; j++)
                            {
                                if (j == pantallas2[w].GetComponent<IDBotones>().iD)
                                {
                                    tiempoParaSaberElReseteo += Time.deltaTime;
                                    boton.boton2[j].SetActive(true);

                                }
                                else
                                {
                                    boton.boton2[j].SetActive(false);
                                    StartCoroutine("TiempoDeEsperaParaBorrarLaListaYResetearLosValores");

                                }

                            }
                        }
                    }
                }
            }
        }
    }
    public void LogicaDeLasCapas(int mayor)
    {
        for(int i = 0; i < pantallas.Length; i++)
        {
            if(pantallas[mayor].GetComponent<SpriteRenderer>().sortingOrder > pantallas[i].GetComponent<SpriteRenderer>().sortingOrder)
            {

            }
            else
            {
                pantallas[mayor].GetComponent<SpriteRenderer>().sortingOrder += pantallas[i].GetComponent<SpriteRenderer>().sortingOrder;
            }
        }
    }
    public static void LogicaPestañas(int id)
    {
        for (int i = 0; i < boton4.Length; i++)
        {
            if (i == id)
            {
                boton4[i].SetActive(false);
            }
            else if(EntendimientoPestañas.eliminado[i])
            {
                boton4[i].SetActive(true);
            }
        }
    }
    public static void LogicaConPestañas2(int ragnarok)
    {
        for (int i = 0; i < boton4.Length; i++)
        {
            if (i == ragnarok)
            {
                boton4[i].SetActive(true);
            }
            else
            {
                boton4[i].SetActive(false);
            }
        }
    }

    public static void LogicaPestañasDefault(int patata)
    {
        if (EntendimientoPestañas.eliminado[patata])
        {
            for (int i = 0; i < boton4.Length; i++)
            {
                boton4[patata].SetActive(true);
            }
        }
        
    }
    IEnumerator TiempoDeEsperaParaBorrarLaListaYResetearLosValores()
    {
        yield return new WaitForSecondsRealtime(0f);
        pantallas2.Clear();
        numPantallas2 = 0;
        indiceComparativo = 0;
        /*for(int g = 0; g < pantallas2.Count; g++)
        {
            pantallas2.Remove(pantallas2[0]);
            numPantallas2 = 0;
            indiceComparativo = 0;

        }*/

    }
}
