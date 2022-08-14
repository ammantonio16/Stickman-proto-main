using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmCard : MonoBehaviour
{
    public int indexMod;
    [SerializeField] ActivarModificador confirm; //Separar Los activadores y pooner cada uno en su respectiva tarjeta
    SpriteRenderer confirmButton;

    private void Awake()
    {
        confirmButton = GetComponent<SpriteRenderer>();
     }
    void Start()
    {
        //You made de LoadStatus for not increase in OpenReforcedDoor Totalkeys once again when you change in scene
        LoadStatus();
    }

    // Update is called once per frame
    void Update()
    {
        ConfirmInsertCard();
    }
    void ConfirmInsertCard()
    {
        if (confirm.modificacion)
        {
            StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
            confirmButton.color = Color.green;
        }
    }
    private void OnDestroy()
    {
        Modificacion.SaveEstado(indexMod, confirm.modificacion);   
    }
    void LoadStatus()
    {
        confirm.modificacion = StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion;
    }
}
