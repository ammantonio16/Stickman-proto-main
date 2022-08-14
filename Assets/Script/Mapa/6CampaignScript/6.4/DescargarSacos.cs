using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescargarSacos : MonoBehaviour
{
    [Header("Elevator Info")]
    [SerializeField] Polipasto_Normal platform;
    [SerializeField] PlatformEnCarga platformInCharge;
    public int indexPosPlatform;

    [Header ("Soldier Info")]
    [SerializeField] SoldadoNormal descargaSoldier;
    [SerializeField] GameObject sacoSoldier;
    [SerializeField] GameObject sacoPlatform;

    [Header ("Time realize action")]
    public float timeCargarLimite;
    public float timeDefaultWaitSoldier;
    float timeCargar;

    int oneAction;
    bool descargaActive;
    void Start()
    {
        //Faltaria hacer los otros dos triggers que te ponen el saco
    }

    // Update is called once per frame
    void Update()
    {
        
        //El polipasto tiene que llegar
        if (descargaSoldier.GetComponent<SoldierLife>().vida > 0)
        {
            PlatformActive();
            if (oneAction < 1)
            {
                if (platformInCharge.cargo)
                {
                    DescargarBags();
                }
                else if (!descargaSoldier.berserker && platformInCharge.cargo)
                {
                    descargaSoldier.maxTiempoUbi = 90;

                }
                //Ver si cuando lo colocas pasa a 90
            }
        }

        if (descargaSoldier.berserker)
        {
            descargaSoldier.maxTiempoUbi = timeDefaultWaitSoldier;
        }
    }

    void DescargarBags()
    {
        if (descargaActive && platform.transform.position == platform.destinos[indexPosPlatform].position)
        {
            timeCargar += Time.deltaTime;
            if (timeCargar >= timeCargarLimite)
            {
                //Descargo
                platformInCharge.cargo = false;
                sacoPlatform.SetActive(false);
                sacoSoldier.SetActive(true);
                descargaActive = false;
                platform.enabled = true;
                timeCargar = 0;
                oneAction++;
                descargaSoldier.maxTiempoUbi = timeDefaultWaitSoldier;

            }
        }

    }
    void PlatformActive()
    {
        //If the platform is in her position and is in charge, desactive the component to wait 
        if (platform.transform.position == platform.destinos[indexPosPlatform].position && platformInCharge.cargo) platform.enabled = false;
        //Make a number that change when the enemy enter and active the platform again if soldiers are life 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //I need know the state of chage if one of the enemy died 
            descargaActive = true;
            oneAction = 0;
            if(!descargaSoldier.berserker)descargaSoldier.maxTiempoUbi = 90;
            //EL cambio lo hace rapidisimo
            //platform.enabled = true;


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //I need know the state of chage if one of the enemy died 
            descargaActive = false;
            
            //oneAction = 0;

            //platform.enabled = true;


        }
    }
}

 
