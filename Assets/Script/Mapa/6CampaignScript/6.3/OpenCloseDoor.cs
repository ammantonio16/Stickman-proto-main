using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    ActivarModificador activador;
    public int indexMod;
    [SerializeField] SpriteRenderer doorClose;
    [SerializeField] Sprite doorOpen;

    private void Awake()
    {
        activador = GetComponent<ActivarModificador>();
    }
    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) OpenWithKey();
    }

    //NOTA ActivarModificador sirve para activar la accion una vez en escena
    
    void Update()
    {
        if (activador.modificacion) OpenWithKey();
    }
    void OpenWithKey()
    {
        doorClose.sprite = doorOpen;
        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
        doorClose.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.SetActive(false);
        //Desactivar Collider y este objeto
    }
}
