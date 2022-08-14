using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonModOfOriginal : MonoBehaviour
{
    SaveButton myInfoButton;
    public SaveButton originalButton;

    void Start()
    {
        //Una vez aparece el boton modificado, traspasara los datos originales a los suyos dado que es "El mismo boton"
        myInfoButton = GetComponent<SaveButton>();
        SaveScene.instancia.listaItemsNivelGuardar[myInfoButton.indexButton].ubiPosButton = SaveScene.instancia.listaItemsNivelGuardar[originalButton.indexButton].ubiPosButton;
        myInfoButton.GetComponent<RectTransform>().localPosition = InventoryManager.instanciaInventory.inventoryPosition[SaveScene.instancia.listaItemsNivelGuardar[originalButton.indexButton].ubiPosButton];

    }

}
