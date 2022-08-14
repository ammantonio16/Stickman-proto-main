using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenReforcedDoor : MonoBehaviour
{
    [SerializeField] List<ActivarModificador> keysRequired;
    [SerializeField] Camino_CambiarZona doorClose;

    int keys;
    public int totalKeys;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        ConfirmedOpenDoor();
    }

    void ConfirmedOpenDoor()
    {
        if(keysRequired.Count > 0)
        {
            for (int i = 0; i < keysRequired.Count; i++)
            {
                //If I try use the statusGameObjectList TotalKeys increase always
                if (keysRequired[i].modificacion)
                {
                    keys++;
                    keysRequired.Remove(keysRequired[i]);
                }
            }
        }
        if(keys == totalKeys)
        {
            doorClose.enabled = true;
        }

    }
}
