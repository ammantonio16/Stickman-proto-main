using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostB8 : MonoBehaviour
{
    public GameObject escombrosB8;

    [Header("Spawn Escombros")]
    public Transform left;
    public Transform right;
    void Start()
    {
        DecideTheSide();
        IdentificarPosB8();
        B8Rota();
    }
    void B8Rota()
    {
        //Previously pick up the object
        //no puedes hacerlo en base al spawn dado que cuendo entres de nuevo ya no sera 1 o 2
        if(!SaveScene.instancia.b8 && SaveScene.instancia.b8Reogida >= 1)
        {
            escombrosB8.SetActive(true);
        }
    }
    void IdentificarPosB8()
    {
        if (SaveScene.instancia.izqEscombrosB8)
        {
            //si tu personaje lo lanzo desde la derecha
            escombrosB8.transform.position = left.position;
        }
        else
        {
            //si tu personaje lo lanzo desde la izquierda
            escombrosB8.transform.position = right.position;
        }
    }
    void DecideTheSide()
    {
        if(CheckPointController.numeroCheckPoint == 1)
        {
            SaveScene.instancia.izqEscombrosB8 = true;
        }
        else if (CheckPointController.numeroCheckPoint == 2)
        {
            SaveScene.instancia.izqEscombrosB8 = false;
        }
    }
}
