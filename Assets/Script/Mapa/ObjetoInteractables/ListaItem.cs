using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaItem : MonoBehaviour
{   
    public List<ListaTodosObjetos> listaItemsNivel;

    private void Start()
    {
        LoadList();

    }
    private void OnDestroy()
    {
        SaveList();
    }
    void SaveList()
    {
        SaveScene.instancia.listaItemsNivelGuardar = listaItemsNivel;
    }
    void LoadList()
    {
        listaItemsNivel = SaveScene.instancia.listaItemsNivelGuardar;
        
    }
    
}
