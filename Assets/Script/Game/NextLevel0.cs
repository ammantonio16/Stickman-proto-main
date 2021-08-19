using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel0 : MonoBehaviour
{
    public Collider2D sigNivel;
    public Animator siguienteNivel;
    public Canvas pantallaGanar;
    ControlDesbloqueoNiveles unlock;
    public int numeroNivel;

    private void Awake()
    {
        unlock = GameObject.Find("ControlDesbloqueoDeNiveles").GetComponent(typeof(ControlDesbloqueoNiveles)) as ControlDesbloqueoNiveles;
    }
    public void OnTriggerEnter2D(Collider2D colli)
    {
        if (colli.gameObject.tag == ("Player"))
        {
            pantallaGanar.SetActive(true);
            siguienteNivel.SetTrigger("Marca Ganar");
            unlock.DesbloquearNiveles();
            StartCoroutine("ActivarListaDeNiveles");
            
            
        }
    }
    IEnumerator ActivarListaDeNiveles()
    {
        yield return new WaitForSeconds(1f);
        ControlDesbloqueoNiveles.listaDeNiveles[numeroNivel] = true;

    }

}
