using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject PrePartida;
    public GameObject Opciones;
    public GameObject title;
    public GameObject SeleccionCampaña;

    #region //Paneles
    public void PrePartidaPanel()
    {
        PrePartida.SetActive(true);
    }

    public void OpcionesPanel()
    {
        Opciones.SetActive(true);
    }

    public void AtrasOpciones()
    {
        title.SetActive(true);
        Opciones.SetActive(false);
    }

    public void AtrasPrePartida()
    {
        title.SetActive(true);
        PrePartida.SetActive(false);
    }

    public void AtrasSeleccionCampaña()
    {
        SeleccionCampaña.SetActive(false);
        PrePartida.SetActive(true);
    }

    public void Campaña()
    {
        PrePartida.SetActive(false);
        SeleccionCampaña.SetActive(true);
    }
    #endregion

    #region //escenas
    public void ComenzarSurvival()
    {
        SceneManager.LoadScene("Survival");
        Time.timeScale = 1f;
    }

    public void EscenaC1()
    {
        SceneManager.LoadScene("Campaign1");
    }

    public void EscenaC2()
    {
        SceneManager.LoadScene("Campaign2");
    }

    public void EscenaC3()
    {
        SceneManager.LoadScene("Campaign3");
    }

    public void EscenaC4()
    {
        SceneManager.LoadScene("Campaign4");
    }

    public void EscenaC5()
    {
        SceneManager.LoadScene("Campaign5");
    }

    public void EscenaC6()
    {
        SceneManager.LoadScene("Campaign6");
    }

    public void EscenaC7()
    {
        SceneManager.LoadScene("Campaign7");
    }

    public void EscenaC8()
    {
        SceneManager.LoadScene("Campaign8");
    }
#endregion 
}
