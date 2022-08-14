using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjetoSoldado : MonoBehaviour
{
    ListaItem listaItemNivel;

    SoldierLife soldadoVida;

    public int objetoARecoger;
    public GameObject objectToPickUp;

    PlayerHere zona;

    public GameObject buttonObjectInventory;

    private void Awake()
    {
        zona = GetComponent<PlayerHere>();

        listaItemNivel = FindObjectOfType<ListaItem>();

        soldadoVida = GetComponentInParent<SoldierLife>();
    }
    private void Start()
    {
        if(SaveScene.instancia.listaItemsNivelGuardar[objetoARecoger].objetoObtenido) objectToPickUp.SetActive(false);
    }
    private void Update()
    {
        if (zona.playerImHere) ObjetoSoldado();
    }
    void ObjetoSoldado()
    {
        if (soldadoVida.vida <= 0)
        {
            Debug.Log("Puedes coger el Objeto con T");
            //Si todavia no has recogido el objeto puedes cogerlo
            CatchObject();

        }
    }
    void CatchObject()
    {
        if (Input.GetKeyDown(KeyCode.T) && !SaveScene.instancia.listaItemsNivelGuardar[objetoARecoger].objetoObtenido)
        {
            listaItemNivel.listaItemsNivel[objetoARecoger].objetoObtenido = true;

            buttonObjectInventory.GetComponent<RectTransform>().localPosition = InventoryManager.instanciaInventory.inventoryPosition[InventoryManager.instanciaInventory.indexInventoryPosition];
            SaveScene.instancia.listaItemsNivelGuardar[objetoARecoger].ubiPosButton = InventoryManager.instanciaInventory.indexInventoryPosition;
            buttonObjectInventory.SetActive(true);
            InventoryManager.instanciaInventory.indexInventoryPosition++;
            Debug.Log("Has cogido la llave");
            objectToPickUp.SetActive(false);
        }
    }

}
