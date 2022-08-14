using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCofre : MonoBehaviour
{
    public int identity;
    [SerializeField] ActivarModificador activar;
    SpriteRenderer spriteActual;
    [SerializeField] Sprite chestOpen;
    private void Awake()
    {
        activar = GetComponent<ActivarModificador>();
        spriteActual = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        LoadEstado();
    }

    void Update()
    {
        //Cambia el Sprite para simular que algo se ha abierto  
        if (activar.modificacion) { spriteActual.sprite = chestOpen; }
    }
    private void OnDestroy()
    {
        Modificacion.SaveEstado(identity, activar.modificacion);
    }
    public void LoadEstado()
    {
        //no Coge el bool
        
        activar.modificacion = StatusGameobjectsVariables.statusGameobject.modificacion[identity].modificacion;
    }
}
