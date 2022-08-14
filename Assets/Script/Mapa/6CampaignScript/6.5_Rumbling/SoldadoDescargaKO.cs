using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldadoDescargaKO : MonoBehaviour
{
    public int indexModSoldadoDescarga;
    [SerializeField] GameObject soldadoDescarga;

    void Start()
    {
        //If soldierDescarga died in drill Scene the rocks not appear 
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexModSoldadoDescarga].modificacion) soldadoDescarga.SetActive(false);
        else 
        {
            soldadoDescarga.SetActive(true);
        }
    }
}
