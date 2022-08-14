using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespertarSoldiers : MonoBehaviour
{
    [SerializeField] KillAllEnemy conditions;
    
    void Update()
    {
        WakeUpSoldiers();
    }
    void WakeUpSoldiers()
    {
        if(conditions.numSoldiers > 0)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                for (int i = 0; i < conditions.soldiersDeath.Count; i++)
                {
                    conditions.soldiersDeath[i].GetComponent<SoldierLife>().vida = 1;
                }
            }
        }
    }
}
