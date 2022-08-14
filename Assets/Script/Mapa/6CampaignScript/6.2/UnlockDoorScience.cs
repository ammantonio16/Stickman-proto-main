using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorScience : MonoBehaviour
{
    public int indexMod;
    public Camino_CambiarZona doorScience;
    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) doorScience.enabled = true;
    }
}
