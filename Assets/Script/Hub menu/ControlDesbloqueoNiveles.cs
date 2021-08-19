using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlDesbloqueoNiveles : MonoBehaviour
{
    public static int nivelesDesbloqueados;
    public int actualLevel;
    public static int numeroMovimientos  = 0;
    public GameObject[] recorridoNiveles;

    //La lista de "bool" se hace para que "DesbloquearNiveles" solo se ejecute un vez y no de problemas si te quieres pasar un nivel varias veces.
    public static bool[] listaDeNiveles = new bool[16];
    void Start()
    {
        //Cada vez que el Player entre al mapa se actualizara la escena
        if(SceneManager.GetActiveScene().buildIndex == 20)
        {
            Actualizar();
        }
    }
    public void DesbloquearNiveles()
    {
        //Esta acción es la que desbloqueara el nivel y solo se ejecutará una vez te hayas pasado un nivel.
        if (!listaDeNiveles[actualLevel])
        {
            /*Si "i" es más pequeño que el "actualLevel", se igualaran para que tu desde el mapa puedas viajar al siguiente nivel,
              porque siempre que empiezas un nivel nuevo "i" va a ser menor que "actualLevel"*/
            if (NivelesMapa.i < actualLevel)
            {
                NivelesMapa.i = actualLevel;
                //actualLevel++;
                //"numeroMovimientos" aumenta para que la condición dentro de "Actualizar" se cumpla y vaya generándose el recorrido. 
                numeroMovimientos++;
            }
        }
        //Una vez te has pasado el nivel no hay nada que desbloquear
        if (listaDeNiveles[actualLevel])
        {
            Debug.Log("El" + " " + actualLevel + " " + "ahora es verdadero" + " " + listaDeNiveles[actualLevel]);
        }
    }
    //La actualización solo sirve para que cada vez que te pases un nivel aparece el recorrido para el siguiente.
    public void Actualizar()
    {
        /*El bucle "for" se hace para saber si el Player ha jugado varios niveles sin a ver pasado por el mapa antes.
          Esto calcula el número de niveles que se ha pasado y dependiendo del número activa los recorridos*/
        for (int w = 0; w < numeroMovimientos + 1; w++)
        {
            recorridoNiveles[w].SetActive(true);
        }
    }

}
