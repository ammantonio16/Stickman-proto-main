using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyedHelmet : MonoBehaviour
{
    public int indexItemMod;

    [SerializeField] PutGasMask modHelmet;
    void Start()
    {
        ChangeHelmet();
    }
    //Maybe only we need put one in scene not in all
    void Update()
    {
        ChangeHelmet();
    }
    void ChangeHelmet()
    {
        if (SaveScene.instancia.listaItemsNivelGuardar[indexItemMod].objetoObtenido)
        {
            modHelmet.enabled = true;
            this.enabled = false;
        }
    }
}
