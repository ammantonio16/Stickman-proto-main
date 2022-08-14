using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modificacion : MonoBehaviour
{

    public static void SaveEstado(int index, bool modificacion)
    {
        StatusGameobjectsVariables.statusGameobject.modificacion[index].modificacion = modificacion;
    }
    
}

