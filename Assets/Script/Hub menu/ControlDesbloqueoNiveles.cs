using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlDesbloqueoNiveles : MonoBehaviour
{
    public static int nivelesDesbloqueados;
    public int actualLevel;
    public Button[] botonesTitle;
    void Start()
    {
        actualLevel = SceneManager.GetActiveScene().buildIndex;
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Actualizar();
        }
    }

    public void DesbloquearNiveles()
    {
        if (nivelesDesbloqueados < actualLevel)
        {
            nivelesDesbloqueados = actualLevel;
            actualLevel++;
        }
    }
    public void Actualizar()
    {
        for (int i = 0; i < nivelesDesbloqueados + 1; i++)
        {
            botonesTitle[i].interactable = true;
        }
    }

}
