using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascensor_Panel : MonoBehaviour
{
    public AscensorMinero ascensorMinero;

    ListaItem listaObjetos;

    [HideInInspector] public bool canUseAscensor;

    public int indexMod;

    ActivarModificador activarMod;
    private void Awake()
    {
        activarMod = GetComponent<ActivarModificador>();
        listaObjetos = FindObjectOfType<ListaItem>();
    }
    private void Start()
    {
        LoadMod();
    }

    private void Update()
    {
        if(activarMod.modificacion) UsarAscensor();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (listaObjetos.listaItemsNivel[0].objetoObtenido)
            {
                canUseAscensor = true;
            }
            else Debug.Log("Requiere de una llave para activar");
        }

    }
    void UsarAscensor()
    {
        if (canUseAscensor)
        {
            if (Input.GetKeyDown(KeyCode.E)) ascensorMinero.interruptorBajar = !ascensorMinero.interruptorBajar;
        }
    }
    private void OnDestroy()
    {
        SaveMod();
    }
    void LoadMod()
    {
        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = activarMod.modificacion;
    }
    void SaveMod()
    {
        activarMod.modificacion = StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion;
    }
}
