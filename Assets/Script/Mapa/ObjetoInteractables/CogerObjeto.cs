using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    ListaItem listaItemNivel;

    public int objetoARecoger;
    public GameObject objectCatch;

    ActivarModificador objetoUsado;

    PlayerHere dentroDeZona;

    public GameObject objetcButtonInventory;
    public enum EstadoObjeto 
    {
        abierto,
        cerrado
    }
    public EstadoObjeto estadoObjeto;
    private void Awake()
    {
        dentroDeZona = GetComponent<PlayerHere>();
        //Only when object is close
        objetoUsado = GetComponent<ActivarModificador>();
        listaItemNivel = FindObjectOfType<ListaItem>();

    }
    private void Start()
    {
        if(SaveScene.instancia.listaItemsNivelGuardar[objetoARecoger].objetoObtenido) objectCatch.SetActive(false);
    }
    private void Update()
    {
        if (dentroDeZona.playerImHere) EstadoDelObjeto();
    }
    void EstadoDelObjeto()
    {

        switch (estadoObjeto)
        {
            
            case EstadoObjeto.abierto:
                Debug.Log("Puedes coger el Objeto con T");
                PickUpObject();
                break;
            case EstadoObjeto.cerrado:

                //SI has usado el objeto Correspondiente
                if (objetoUsado.modificacion)
                {
                    Debug.Log("Puedes coger el Objeto con T");
                    PickUpObject();
                }
                else { Debug.Log("Necesitas una llave"); }
                break;
        }



    }
    void PickUpObject()
    {
        if (Input.GetKeyDown(KeyCode.T) && !SaveScene.instancia.listaItemsNivelGuardar[objetoARecoger].objetoObtenido)
        {
            //Take The object
            listaItemNivel.listaItemsNivel[objetoARecoger].objetoObtenido = true;
            //Set the button position and active
            objetcButtonInventory.GetComponent<RectTransform>().localPosition = InventoryManager.instanciaInventory.inventoryPosition[InventoryManager.instanciaInventory.indexInventoryPosition];
            SaveScene.instancia.listaItemsNivelGuardar[objetoARecoger].ubiPosButton = InventoryManager.instanciaInventory.indexInventoryPosition;
            objetcButtonInventory.SetActive(true);
            //Increaso to the next position for the following object
            InventoryManager.instanciaInventory.indexInventoryPosition++;
            //Disappaer the object
            objectCatch.SetActive(false);
        }
       
    }
}
