using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerReliquias : MonoBehaviour
{
    public int numeroReliquia;

    bool estoyEnLaReliquia;
    //Cuando coges la reliquia desaparece para siempre no al cogerlo
    private void Update()
    {
        if (estoyEnLaReliquia) PickReliquia();
    }
    void PickReliquia()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        { 
            ListaReliquias.listaReliquiasTotal[numeroReliquia].reliquiaObtenida = true;
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) estoyEnLaReliquia = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) estoyEnLaReliquia = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) estoyEnLaReliquia = false;
    }
}
