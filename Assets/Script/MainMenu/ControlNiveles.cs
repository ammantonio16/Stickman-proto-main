using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlNiveles : MonoBehaviour
{
    public Animator repetirEnd;
    
    public void RepeatScene (int numeroEscena)
    {
        repetirEnd.SetBool("Repetir", true);
        StartCoroutine(RecargarEscena(numeroEscena, 2f));
    }
    IEnumerator RecargarEscena(int escena, float tiempoRecarga)
    {
        yield return new WaitForSeconds(tiempoRecarga);
        SceneManager.LoadScene(escena);
    }
    public void SiguienteNivel(int index)
    {
        repetirEnd.SetBool("Pasar Nivel", true);
        StartCoroutine(RecargarEscena(index, 2f));
    }
    public void SalirNivel(int menu)
    {
        repetirEnd.SetBool("Salir", true);
        StartCoroutine(RecargarEscena(menu, 2f));
    }
}
