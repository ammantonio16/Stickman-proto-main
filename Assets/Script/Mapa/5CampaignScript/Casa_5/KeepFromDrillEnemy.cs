using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFromDrillEnemy : MonoBehaviour
{
    [SerializeField] Transform safeUbi;
    SoldadoNormal soldadoNormal;

    [SerializeField] List<Transform> copyUbiDirigir;


    int sizeUbiDirigirse;
    int reset;

    //NOTE: You can't do list = list, because if you did, will link the two list and as a result when you modified one the other made the same 
    private void Awake()
    {
        soldadoNormal = GetComponent<SoldadoNormal>();
        sizeUbiDirigirse = soldadoNormal.ubicacionesDirigir.Count;
    }
    void Update()
    {
        if (EncenderTaladro.encender) 
        {
            SafePlace();
            reset = 0;
        }
        else
        {
            if (reset < 1)
            {
                NoDanger();
                reset++;
            }
        }
    }
    void SafePlace()
    {
        soldadoNormal.ubicacionesDirigir.Clear();
        soldadoNormal.ubicacionesDirigir.Add(safeUbi);
    }
    void NoDanger()
    {
        soldadoNormal.ubicacionesDirigir.Clear();
        for (int i = 0; i < sizeUbiDirigirse; i++)
        {
            soldadoNormal.ubicacionesDirigir.Add(copyUbiDirigir[i]);
        }
        
    }
}
