using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpSoldier : MonoBehaviour
{
    [SerializeField] SoldierLife soldierGetUpLife;
    [SerializeField] SoldierLife soldierWhoGetUp;
    [SerializeField] Somnolencia soldierGetUpSom;

    public int indexMod;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) GetUpExit();
    }
    private void OnDestroy()
    {
        ConditionGetUp();
    }
    void ConditionGetUp()
    {
        //The drill on and soldier "Dead"
        if (soldierGetUpLife.vida < 1 && soldierWhoGetUp.vida > 0 && EncenderTaladro.encender) 
        {
            StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
            Debug.Log("Las condiciones SE cumplen");
        } 
        else 
        {
            StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = false;
            Debug.Log("Las condiciones no se cumplen");
        }
    }
    void GetUpExit()
    {
        //Soldier recover life to get up;
        soldierGetUpLife.vida = 1;

        //The soldier status change from sleep always to sleep moderately. So the player will have opportunity to collect terminal, if he has forgotten
        soldierGetUpSom.tiempoLimiteDespierto = 4;

        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = false;

    }
}
