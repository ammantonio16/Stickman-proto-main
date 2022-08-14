using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveTakeWater : MonoBehaviour
{
    public int indexModToInactive;
    [SerializeField] SustitucionObjeto sustitutoPozo;

    // Update is called once per frame
    void Update()
    {
        InactiveWater();
    }
    public void InactiveWater()
    {
        //If you drain water from pit, you can take water from him
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexModToInactive].modificacion)
        {
            sustitutoPozo.enabled = false;
        }
    }
}
