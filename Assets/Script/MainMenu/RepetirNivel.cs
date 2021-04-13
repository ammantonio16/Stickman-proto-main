using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepetirNivel : MonoBehaviour
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
}
