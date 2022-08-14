using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppearKey : MonoBehaviour
{
    SoldierLife soldadoVida;
    [SerializeField] KillAllEnemy allSoldiers;
    bool oneTime;
    void Start()
    {
        oneTime = true;
        soldadoVida = GetComponent<SoldierLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oneTime) DroppearItem();
    }

    void DroppearItem()
    {
        if(soldadoVida.vida <= 0)
        {
            allSoldiers.soldiersDeath.Add(this.gameObject);
            allSoldiers.numSoldiers--;
            oneTime = false;
        }
    }
}
