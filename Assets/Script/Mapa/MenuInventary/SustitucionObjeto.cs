using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SustitucionObjeto : MonoBehaviour
{
    public string tagName;

    bool playerImHere;
    bool gameObjectImHere;

    public SaveButton baseButton;
    public int indexObjetoOriginal;
    public SaveButton baseButtonMod;
    public int indexObjetoMod;


    private void Update()
    {
        if(gameObjectImHere && playerImHere)
        {
            if (Input.GetButtonDown("Fire1")) SustituirObjeto();
        }
    }
    void SustituirObjeto()
    {
        //Cambias el boton original por el modificado, para dar entender que se ha hecho esa accion con el objeto pero este no se ha perdido
        baseButton.gameObject.SetActive(false);
        baseButtonMod.gameObject.SetActive(true);
        //Coger el objeto de la lista
        SaveScene.instancia.listaItemsNivelGuardar[indexObjetoMod].objetoObtenido = true;
        SaveScene.instancia.listaItemsNivelGuardar[indexObjetoOriginal].objetoObtenido = false;
        
        //Coloca el boton en la misma posicion que el original ya almacenado previamente
        baseButtonMod.GetComponent<RectTransform>().localPosition = InventoryManager.instanciaInventory.inventoryPosition[SaveScene.instancia.listaItemsNivelGuardar[baseButtonMod.indexButton].ubiPosButton];


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) gameObjectImHere = true;
        if (collision.gameObject.CompareTag("Player")) playerImHere = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) gameObjectImHere = true;
        if (collision.gameObject.CompareTag("Player")) playerImHere = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) gameObjectImHere = false;
        if (collision.gameObject.CompareTag("Player")) playerImHere = false;
    }
}
