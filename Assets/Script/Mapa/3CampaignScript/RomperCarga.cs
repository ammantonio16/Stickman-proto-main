using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomperCarga : MonoBehaviour
{
    [SerializeField] SoldierLife soldadoCarga;
    [SerializeField] SoldierLife soldadoDescarga;
    [Header("Ascensor Normal")]
    [HideInInspector]public bool ascensorNormal;

    [Header("Desactivar Mover Cajas")]
    ZonaCarga zonaCarga;
    ZonaDescarga zonaDescarga;
    Cargas cargar;
    private void Awake()
    {
        zonaCarga = FindObjectOfType<ZonaCarga>();
        zonaDescarga = FindObjectOfType<ZonaDescarga>();
        cargar = FindObjectOfType<Cargas>();
    }

    // Update is called once per frame
    void Update()
    {
        RomperRutina();
    }
    void RomperRutina()
    {
        //Uno de los dos esta KO || Si uno de los dos esta en modo ataque || Si estan en modo alerta
        if (soldadoCarga.vida <= 0 || soldadoDescarga.vida <= 0 ||
            soldadoCarga.GetComponent<SoldadoNormal>().berserker || soldadoDescarga.GetComponent<SoldadoNormal>().berserker ||
            soldadoCarga.GetComponent<SoldadoNormal>().seeCadaverWarning || soldadoDescarga.GetComponent<SoldadoNormal>().seeCadaverWarning) ascensorNormal = true;

        //Si ambos estan KO
        if (soldadoCarga.vida <= 0 && soldadoDescarga.vida <= 0) { zonaCarga.enabled = false; zonaDescarga.enabled = false; cargar.enabled = false; }

    }
}
