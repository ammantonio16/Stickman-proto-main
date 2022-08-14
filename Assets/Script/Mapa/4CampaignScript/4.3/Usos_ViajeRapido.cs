using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usos_ViajeRapido : MonoBehaviour
{
    bool caminoRapido;

    int atravesarUnaVez;
    public enum TipoDeViajeRapido
    {
        Boton,
        Atravesando
    }

    public TipoDeViajeRapido maneraDeViajar;
    private void Update()
    {
        switch (maneraDeViajar)
        {
            case TipoDeViajeRapido.Boton:
                UsarViajeRapidoBoton();
                break;
            case TipoDeViajeRapido.Atravesando:
                UsarViajeRapidoAtravesando();
                break;
        }
    }

    // Update is called once per frame
    void UsarViajeRapidoBoton()
    {
        if (caminoRapido && TotalTerminales.terminalesTotales.terminalesActivados >= TotalTerminales.terminalesTotales.terminalesActivos.Count)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SaveScene.instancia.usosViajeRapido--;
            }
        }
    }
    void UsarViajeRapidoAtravesando() 
    {
        if (caminoRapido && TotalTerminales.terminalesTotales.terminalesActivados >= TotalTerminales.terminalesTotales.terminalesActivos.Count)
        {
            if(atravesarUnaVez < 1)
            {
                SaveScene.instancia.usosViajeRapido--;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caminoRapido = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caminoRapido = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caminoRapido = true;
        }
    }
}
