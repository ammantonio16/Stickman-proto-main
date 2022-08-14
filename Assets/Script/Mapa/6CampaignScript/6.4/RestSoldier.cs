using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSoldier : MonoBehaviour
{
    public int indexMod;
    public int indexModDescarga;

    [Header("Soldier And Items EXchange in Rest")]
    public List<GameObject> soldierRest;

    [Header("Soldier Minero")]
    [SerializeField] SoldadoNormal mineroCarga;
    [SerializeField] Transform restPoint;
    [Header("Soldier Descarga")]
    public int indexSoldadoDescarga;
    [SerializeField] SoldierLife soldadoDescarga;


    void Awake()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) 
        {
            SoldadoOut();
            Rest();
        }

    }
    void Rest()
    {
        foreach(GameObject soldier in soldierRest)
        {
            soldier.SetActive(false);
        }


        mineroCarga.ubicacionesDirigir.Clear();
        mineroCarga.ubicacionesDirigir.Add(restPoint);
    }
    void SoldadoOut()
    {
        if (SoldierActiveInScene.instancia.soldiersStatus[indexSoldadoDescarga].statusVida > 0)
        {
            Debug.Log("Muy Out");
            soldadoDescarga.gameObject.SetActive(false);
        }
        //if you died before it rings the bell, when activate rumbling the rocks don't fall
        else { StatusGameobjectsVariables.statusGameobject.modificacion[indexModDescarga].modificacion = true; }
    }
}
