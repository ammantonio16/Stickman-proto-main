using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaConMovil : MonoBehaviour
{
    public Animator movilAn;
    public float time;
    public GameObject movil;
    public Animator repetirEnd;
    public GameObject[] botones;
    void Start()
    {

        botones = GameObject.FindGameObjectsWithTag("Botones");
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Pausa()
    {
        //Time.timeScale = 0;
        movilAn.SetBool("PA", true);
        StartCoroutine(TiempoPausa(time));
        movil.SetActive(false);
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].SetActive(true);
        }
    }
    IEnumerator TiempoPausa(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Time.timeScale = 0;
    }
    IEnumerator TiempoContinuar(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        movil.SetActive(true);
    }
    public void Continuar()
    {
        movilAn.SetBool("Volver", true);
        Time.timeScale = 1;
        movilAn.SetBool("PA", false);
        StartCoroutine(TiempoContinuar(2f));
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].SetActive(false);
        }

    }
    public void RepeatScene(int numeroEscena)
    {
        Time.timeScale = 1;
        repetirEnd.SetBool("Repetir", true);
        StartCoroutine(RecargarEscena(numeroEscena, 2f));
    }
    IEnumerator RecargarEscena(int escena, float tiempoRecarga)
    {
        yield return new WaitForSeconds(tiempoRecarga);
        SceneManager.LoadScene(escena);
    }
    public void SalirNivel(int menu)
    {
        Time.timeScale = 1;
        repetirEnd.SetBool("Salir", true);
        StartCoroutine(RecargarEscena(menu, 2f));
    }
}
