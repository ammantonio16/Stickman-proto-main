using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaAttackSoldado : MonoBehaviour
{
    public static ListaAttackSoldado modeSoldiers;

    public bool activeAllAttackSoldiers;
    public bool warningAllSoldiers;
    //CUando esto se activa se colocan todos los bool modeSoldier en true

    int soldierInAttack;
    int soldierInWarning;

    private void Awake()
    {
        if (ListaAttackSoldado.modeSoldiers == null)
        {
            ListaAttackSoldado.modeSoldiers = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void AllActiveAttack()
    {
        for (int i = 0; i < SoldierActiveInScene.instancia.soldiersStatus.Count; i++)
        {
            if (SoldierActiveInScene.instancia.soldiersStatus[i].modeSoldier)
            {
                soldierInAttack++;
            }
        }
        if (soldierInAttack >= SoldierActiveInScene.instancia.soldiersStatus.Count)
        {
            activeAllAttackSoldiers = true;
        }
    }
    public void AllWarningActive()
    {
        for (int i = 0; i < SoldierActiveInScene.instancia.soldiersStatus.Count; i++)
        {
            if (SoldierActiveInScene.instancia.soldiersStatus[i].warningSoldier)
            {
                soldierInWarning++;
            }
        }
        if (soldierInWarning >= SoldierActiveInScene.instancia.soldiersStatus.Count)
        {
            warningAllSoldiers = true;
        }
    }
    public void UnactiveAllWarning()
    {
        foreach (ListaStatusSoldierScenes statusSoldierMode in SoldierActiveInScene.instancia.soldiersStatus)
        {
            statusSoldierMode.warningSoldier = false;
            soldierInWarning--;
        }
        if (soldierInWarning <= SoldierActiveInScene.instancia.soldiersStatus.Count)
        {
            warningAllSoldiers = false;
        }
    }
}
