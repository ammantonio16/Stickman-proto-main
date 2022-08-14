using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OcultacionByWeapon : MonoBehaviour
{
    [Header("Visible Duration")]
    public float tiempoLimiteVisible;
    float tiempoVisible;
    [HideInInspector] public float defaultTiempoLimiteVisible;
    [Header("Player Data")]
    public int layerPlayer;
    [SerializeField] GameObject playerTorso;
    public Image cooldownUI;
    public Image isVisible;
    [Header("Soldier Fake")]
    public int layerEnemigo;

    [HideInInspector] public bool visible;
    
    void Start()
    {
        defaultTiempoLimiteVisible = tiempoLimiteVisible;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownUI.fillAmount = tiempoVisible / tiempoLimiteVisible;
        OcultacionLost();
    }
    void OcultacionLost()
    {
        //Si disparas puedes ser visto 

        //Llamar a la lista soldado una vez antes ya que el primer golpe no lo registra;
        if (!ListaAttackSoldado.modeSoldiers.activeAllAttackSoldiers)
        {
            if (visible)
            {

                tiempoVisible += Time.deltaTime;

                //Referencia visual
                cooldownUI.fillAmount = (tiempoLimiteVisible - tiempoVisible) / tiempoLimiteVisible;

                //Layer Player;
                playerTorso.layer = layerPlayer;

                //default se utiliza para cuando te han pillado en modo busqueda, restablece el valor original para que no se quede con el incrementado
                if (tiempoVisible >= tiempoLimiteVisible)
                {
                    tiempoVisible = 0;
                    tiempoLimiteVisible = defaultTiempoLimiteVisible;
                    visible = false;
                    playerTorso.layer = layerEnemigo;

                }

            }

        }
        else
        {
            isVisible.enabled = true;
            playerTorso.layer = 20;
        }
    }

    public void VisibleOn()
    {
        playerTorso.layer = layerPlayer;
        isVisible.enabled = true;
    }
    public void VisibleOff()
    {
        playerTorso.layer = layerEnemigo;
        
    }
    //They see if you use Terminal
    public void SeeByAction(bool seePlayerByAction)
    {
        if (seePlayerByAction)
        {
            Debug.Log("Puedo ser VIsto;");
            //Evitar poner un icono diferente
            isVisible.enabled = true;
            playerTorso.layer = 20;
        }
        else
        {
            isVisible.enabled = false;
            playerTorso.layer = layerEnemigo;
        }
    }
}
