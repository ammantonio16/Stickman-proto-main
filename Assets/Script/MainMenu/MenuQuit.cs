using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuQuit : MonoBehaviour
{
    public GameObject PanelTitulo;
    public GameObject PanelCerrarJuego;

   public void cerrar()
    {
        Application.Quit();
        Debug.Log("Saliste del juego exitosamente");
    }

    public void NoCerrar()
    {
        PanelCerrarJuego.SetActive(false);
        PanelTitulo.SetActive(true);
    }

    public void quierescerrar()
    {
       PanelCerrarJuego.SetActive(true);
    }
}
