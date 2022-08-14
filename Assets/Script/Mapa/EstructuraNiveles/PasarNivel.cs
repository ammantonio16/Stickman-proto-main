using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasarNivel : MonoBehaviour
{
    EstructuraNiveles cambiarZona = new EstructuraNiveles();
    int nivelDirigir;
    private void Update()
    {
        nivelDirigir = EstructuraNiveles.nivel;
        
    }

    public void CambiarZona()
    {
        cambiarZona.PasarNivelAnimacion(nivelDirigir);
        Debug.Log("NivelDirigir es" + " " + nivelDirigir);
    }

}
