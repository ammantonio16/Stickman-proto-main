using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierActiveInScene : MonoBehaviour
{
    public static SoldierActiveInScene instancia;

    public List<ListaStatusSoldierScenes> soldiersStatus;

    private void Awake()
    {
        if (SoldierActiveInScene.instancia == null)
        {
            SoldierActiveInScene.instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

}
