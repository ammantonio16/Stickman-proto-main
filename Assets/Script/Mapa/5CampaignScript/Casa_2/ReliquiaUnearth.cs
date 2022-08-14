using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReliquiaUnearth : MonoBehaviour
{
    public int indexModUnearth;
    public int numReliquia;

    [SerializeField] GameObject reliquia;
    void Start()
    {
        //If you made Unearth and no put the reliquia, the reliquia appear
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexModUnearth].modificacion)
        {
            if (!ListaReliquias.listaReliquiasTotal[numReliquia].reliquiaObtenida) reliquia.SetActive(true);
            else reliquia.SetActive(false);

        }
    }
}
