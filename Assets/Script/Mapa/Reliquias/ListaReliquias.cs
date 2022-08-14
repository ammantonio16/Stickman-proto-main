using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaReliquias : MonoBehaviour
{
    public static ListaReliquias instancia;

    public static List<Reliquias> listaReliquiasTotal = new List<Reliquias>();
    public  List<Reliquias> listaReliquiasInspector = new List<Reliquias>();
    private void Awake()
    {
        if (ListaReliquias.instancia == null)
        {
            ListaReliquias.instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        listaReliquiasTotal = listaReliquiasInspector;
    }

    // Update is called once per frame

}
