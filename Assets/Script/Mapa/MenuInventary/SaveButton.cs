using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public int indexButton;
    public int indexPos;

    [Header("Usos del objeto")]
    public int usosLimitados;
    [HideInInspector] public int usos;
    Image imageButton;
    Button button;
    Text textButton;

    private void Awake()
    {
        imageButton = GetComponent<Image>();
        button = GetComponent<Button>();
        textButton = GetComponentInChildren<Text>();
    }
    void Start()
    {
        CallButtonAppear();
        LimitarUsos();
    }
    void CallButtonAppear()
    {

        if (SaveScene.instancia.listaItemsNivelGuardar[indexButton].objetoObtenido) 
        {
            this.gameObject.SetActive(true);
            
            this.gameObject.GetComponent<RectTransform>().localPosition =  InventoryManager.instanciaInventory.inventoryPosition[SaveScene.instancia.listaItemsNivelGuardar[indexButton].ubiPosButton];
        } 
        else this.gameObject.SetActive(false);
    }
    public void LimitarUsos()
    {
        //Coge los usos que estan permitidos usar en la lista y los iguala
        //If you call one time desactive the button always
        usos = SaveScene.instancia.listaItemsNivelGuardar[indexButton].vecesUsadoObjeto;
        if (usos >= usosLimitados)
        {
            UsosGastados();
        }
    }
    public void UsosGastados()
    {
        //Inactive the button
        imageButton.color = new Color(1f, 1f, 1f, 0.25f);
        textButton.color = new Color(0f, 0f, 0f, 0.5f);
        button.enabled = false;
    }
    


}
