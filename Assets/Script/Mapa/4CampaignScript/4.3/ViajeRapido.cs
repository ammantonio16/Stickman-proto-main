using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViajeRapido : MonoBehaviour
{

    [SerializeField] Camino_CambiarZona caminoBoton;
    [SerializeField] Cambiar_Area caminoAtravesar;
    [SerializeField] Loop caminoAtravesarLoop;

    [Header ("Viaje Rapido")]
    public int escenaDeViajeRapido;
    public int checkpointDeViajeRapido;


    public enum TipoViajeFast
    {
        CaminoBoton,
        CaminoAtravesar
    }
    public TipoViajeFast estiloViaje;

    void Update()
    {
        switch (estiloViaje)
        {
            case TipoViajeFast.CaminoBoton:
                ViajeRapidoBoton_4();
                break;
            case TipoViajeFast.CaminoAtravesar:
                ViajeRapidoAtravesando_4();
                break;

        }
    }

    void ViajeRapidoBoton_4()
    {
        if (TotalTerminales.terminalesTotales.terminalesActivados >= TotalTerminales.terminalesTotales.terminalesActivos.Count)
        {
            if(SaveScene.instancia.usosViajeRapido >= 1)
            {
                caminoBoton.checkpointSpanwScene = checkpointDeViajeRapido;
                caminoBoton.siguienteEscena = escenaDeViajeRapido;
            }
            
        }   
    }
    void ViajeRapidoAtravesando_4()
    {
        if (TotalTerminales.terminalesTotales.terminalesActivados >= TotalTerminales.terminalesTotales.terminalesActivos.Count)
        {
            if (SaveScene.instancia.usosViajeRapido >= 1)
            {
                caminoAtravesar.enabled = true;
                caminoAtravesarLoop.enabled = false;
                caminoAtravesar.checkPoint = checkpointDeViajeRapido;
                caminoAtravesar.siguienteEscena = escenaDeViajeRapido;
            }
            else
            {
                caminoAtravesar.enabled = false;
            }
            

        }
    }
}
