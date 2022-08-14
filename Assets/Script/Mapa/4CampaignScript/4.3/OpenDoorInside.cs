using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorInside : MonoBehaviour
{
    public int indexModificador;
    [SerializeField] SpriteRenderer door;
    [SerializeField] Sprite doorOpen;
    private void Start()
    {
        if(StatusGameobjectsVariables.statusGameobject.modificacion[indexModificador].modificacion) AbrirPuertaInside();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Nota: Si en el 4.3 te siguen en la zona de abajo tambien abriran las puertas
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")) 
        {
            AbrirPuertaInside();
        }
    }
    void AbrirPuertaInside()
    {
        door.sprite = doorOpen;
        door.GetComponent<BoxCollider2D>().enabled = false;
        StatusGameobjectsVariables.statusGameobject.modificacion[indexModificador].modificacion = true;
    }
}
