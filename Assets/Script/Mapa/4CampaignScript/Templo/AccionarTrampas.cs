using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionarTrampas : MonoBehaviour
{
    [SerializeField] CogerObjeto cogerObjetoOrbe;

    private void Update()
    {
        if(SaveScene.instancia.activarTrampasTemplo) cogerObjetoOrbe.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (!SaveScene.instancia.listaItemsNivelGuardar[2].objetoObtenido)
            {
                SaveScene.instancia.activarTrampasTemplo = true;
                
            }
            
        } 
    }
}
