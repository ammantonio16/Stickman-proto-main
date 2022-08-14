using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarSacos : MonoBehaviour
{
    [Header("Elevator Info")]
    [SerializeField] Polipasto_Normal platform;
    [SerializeField] PlatformEnCarga platformInCharge;
    public int indexPosPlatform;

    [Header("Soldier Info")]
    [SerializeField] SoldadoNormal cargaSoldier;
    [SerializeField] GameObject sacoSoldier;
    [SerializeField] GameObject sacoPlatform;

    [Header("Time realize action")]
    public float timeCargarLimite;
    public float timeDefaultWaitSoldier;
    float timeCargar;

    int oneAction;
    bool cargaAcive;
    void Start()
    {
        //Faltaria hacer los otros dos triggers que te ponen el saco
    }

    void Update()
    {
        
        //El polipasto tiene que llegar
        if(cargaSoldier.GetComponent<SoldierLife>().vida > 0)
        {
            PlatformActive();
            if (oneAction < 1)
            {
                //If soldier is not berserker and the platform is offload
                if (!platformInCharge.cargo)
                {
                    CargarBags();

                }
                else if (!cargaSoldier.berserker && platformInCharge.cargo)
                {
                    cargaSoldier.maxTiempoUbi = 90;

                }
            }
            
            if (cargaSoldier.berserker)
            {
                cargaSoldier.maxTiempoUbi = timeDefaultWaitSoldier;
            }
        }
    }
    void CargarBags()
    {
        if(cargaAcive && platform.transform.position == platform.destinos[indexPosPlatform].position)
        {
            timeCargar += Time.deltaTime;
            if(timeCargar >= timeCargarLimite)
            {
                platformInCharge.cargo = true;
                sacoPlatform.SetActive(true);
                sacoSoldier.SetActive(false);
                cargaAcive = false;
                platform.enabled = true;
                timeCargar = 0;
                oneAction++;
                cargaSoldier.maxTiempoUbi = timeDefaultWaitSoldier;


            }
        }
        
    }
    void PlatformActive()
    {
        if (platform.transform.position == platform.destinos[indexPosPlatform].position && !platformInCharge.cargo) platform.enabled = false;
        //Make a number that change when the enemy enter and active the platform again if soldiers are life 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //I need know the state of chage if one of the enemy died 
            cargaAcive = true;
            oneAction = 0;
            
            if(!cargaSoldier.berserker)cargaSoldier.maxTiempoUbi = 90;


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            cargaSoldier.maxTiempoUbi = timeDefaultWaitSoldier;
            cargaAcive = false;
        }
    }
}
