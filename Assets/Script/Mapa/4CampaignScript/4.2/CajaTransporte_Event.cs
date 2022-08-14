using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaTransporte_Event : MonoBehaviour
{
    [SerializeField]GameObject cuerpoPlayer;
    [SerializeField]SoldadoNormal soldierCharge;


    float tiempoNecesarioStartEvent;

    bool onBox;

    public int escena_Cinematic;

    void Update()
    {
        Transporte_Event();
    }
    void Transporte_Event()
    {

        //si el soldado que esta cargando las cajas esta vivo y yo oculto
        if(cuerpoPlayer.layer == 31 && !soldierCharge.berserker && soldierCharge.GetComponent<SoldierLife>().vida > 0 && onBox)
        {
            tiempoNecesarioStartEvent += Time.deltaTime;
            if (tiempoNecesarioStartEvent >= 1) 
            {
                Debug.Log("El Evento ha comenzado"); EstructuraNiveles.nivel = escena_Cinematic;
                AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) onBox = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) onBox = false; ;
    }
}
