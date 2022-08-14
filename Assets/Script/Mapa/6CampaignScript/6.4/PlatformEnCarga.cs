using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnCarga : MonoBehaviour
{
    public bool cargo;

    [Header("Time inactive")]
    [SerializeField] Polipasto_Normal plataforma;
    float timeDesactiveCargarSacos;
    bool resetTime;

    [Header("Zona Carga/Descarga")]
    [SerializeField] CargarSacos triggerCarga;
    [SerializeField] DescargarSacos triggerDescarga;

    [Header("Recogida Sacos")]
    public int catchBags;
    public int leaveBags;
    [SerializeField] List<CogerSaco> coger_recoger;

    [Header("Soldiers")]
    [SerializeField] SoldadoNormal soldierDescarga;
    [SerializeField] SoldadoNormal soldierCarga;

    private void Update()
    {
        DesactivarSacos();
        DesactivarSacos2();
        //Añadir Status Variables objetos esto
    }
    //First Condition: one of the soldier is knock out and the other is waiting
    void DesactivarSacos()
    {
        if (cargo)
        {
            if (resetTime)
            {
                timeDesactiveCargarSacos = 0;
                resetTime = false;
            }
            timeDesactiveCargarSacos += Time.deltaTime;
            if(timeDesactiveCargarSacos >= 90)
            {
                triggerCarga.enabled = false;
                triggerDescarga.enabled = false;
                foreach (CogerSaco sacos in coger_recoger)
                {
                    sacos.enabled = false;
                }
            }
        }
        else
        {
            if (!resetTime)
            {
                timeDesactiveCargarSacos = 0;
                resetTime = true;
            }
            if (timeDesactiveCargarSacos >= 90)
            {
                triggerCarga.enabled = false;
                triggerDescarga.enabled = false;
                foreach (CogerSaco sacos in coger_recoger)
                {
                    sacos.enabled = false;
                }

            }
        }
    }
    //Second Condition
    void DesactivarSacos2()
    {
        if(soldierCarga.GetComponent<SoldierLife>().vida <= 0)
        {
            plataforma.enabled = true;
            triggerCarga.enabled = false;
            coger_recoger[catchBags].enabled = false;  
        }
        if(soldierDescarga.GetComponent<SoldierLife>().vida <= 0)
        {
            plataforma.enabled = true;
            triggerDescarga.enabled = false;
            coger_recoger[leaveBags].enabled = false;
        }
    }
}
