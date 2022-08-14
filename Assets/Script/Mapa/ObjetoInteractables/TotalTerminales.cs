using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TotalTerminales : MonoBehaviour
{
    public static TotalTerminales terminalesTotales;


    public List<bool> terminalesActivos;

     [HideInInspector]public bool carga;
     public int terminalesActivados;

     


    private void Awake()
    {
        if (TotalTerminales.terminalesTotales == null)
        {
            TotalTerminales.terminalesTotales = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }

    public void PortatilValueReset()
    {
        TotalTerminales.terminalesTotales.terminalesActivos.Clear();
        TotalTerminales.terminalesTotales.terminalesActivados = 0;
        carga = false;
    }


}
