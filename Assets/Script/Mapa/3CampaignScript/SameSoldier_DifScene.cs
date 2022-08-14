using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameSoldier_DifScene : MonoBehaviour
{
    SoldierLife soldierVida;
    SoldadoNormal soldadoNormal;
    [SerializeField] Transform cambiarUbi; 
    private void Awake()
    {
        soldierVida = GetComponent<SoldierLife>();
        soldadoNormal = GetComponent<SoldadoNormal>();
    }

    private void Start()
    {
        ChangePositionLife();
    }
    void ChangePositionLife()
    {
        if(soldierVida.vida > 0)
        {
            transform.position = cambiarUbi.position;
        }
    }
}
