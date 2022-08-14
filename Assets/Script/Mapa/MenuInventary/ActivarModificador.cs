using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarModificador : MonoBehaviour
{


    public string tagName;
    public bool modificacion;
    bool youCanChange;
    bool playerIsInRange;

    public SaveButton buttonOfItemUse;



    void Update()
    {
        //Si el player esta cerca del objeto y ha sacado el objeto
        if (youCanChange && playerIsInRange)
        {
            Debug.Log("Preparado para la ACCION");
            //Usas el objeto
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("ACCION on");
                UsarObjeto();
            }
            
        }
    }

    void UsarObjeto()
    {

        //Si usas el objeto
        Debug.Log("Debug");
        modificacion = true;
        //Increase the number of uses from object 
        SaveScene.instancia.listaItemsNivelGuardar[buttonOfItemUse.indexButton].vecesUsadoObjeto++;
        //If the object its equal to the max uses, this button becomes inactive
        buttonOfItemUse.LimitarUsos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) youCanChange = true;
        if (collision.gameObject.CompareTag("Player")) playerIsInRange = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) youCanChange = true;
        if (collision.gameObject.CompareTag("Player")) playerIsInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) youCanChange = false;
        if (collision.gameObject.CompareTag("Player")) playerIsInRange = false;
    }
    
}
