using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListaStatusSoldierScenes
{
    // Start is called before the first frame update
    public string SoldierName;
    public int statusVida;
    [HideInInspector]public bool modeSoldier;
    [HideInInspector]public bool warningSoldier;
    [HideInInspector] public float position_x;
    [HideInInspector] public float position_y;
}
