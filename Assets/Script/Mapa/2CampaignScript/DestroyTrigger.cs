using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    public int indexMod;

    [SerializeField] Detectar_y_Echar detector;
    [SerializeField] BloquearMovimientoPlayer playerBlock;
    // Update is called once per frame
    void Update()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) 
        {
            Destroy(detector);
            playerBlock.HabilitarMovPlayer();
        }
        
    }
}
