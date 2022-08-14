using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public bool changeWeapon;
    [Header("B8 Weapon")]
    [SerializeField] B8Arma b8;
    [SerializeField] Image b8Icon;
    [Header("Weapon Base")]
    [SerializeField] ArmaController weaponBase;
    [SerializeField] Image pistolIcon;
    [Header("Ocultacion")]
    [SerializeField] OcultacionByWeapon ocultacion;
    [SerializeField] float timeHiddenB8 = 1;
    [SerializeField] float timeHiddenPistol = 3;
    [Header("Player")]
    public int layerB8;
    [SerializeField] GameObject torso;
    private void Start()
    {
        changeWeapon = false;
        
    }
    void Update()
    {
        if (SaveScene.instancia.b8) Weapons();

    }
    void Weapons()
    {
        Icon();
        if (Input.GetKeyDown(KeyCode.M))
        {
            changeWeapon = !changeWeapon;
            //changeWeapon = B8
            //!changeWeapon = weaponBase
            if (changeWeapon)
            {
                weaponBase.gameObject.SetActive(false);
                weaponBase.municionText.enabled = false;

                b8.gameObject.SetActive(true);
                //You can't draw B8 if you don't have ammo
                if(b8.municionTotal > 0)
                {
                    //Stablish the last mode active in weapon.
                    //if you draw the weapon, when you change will have drawn the gun too
                    b8.activarB8 = weaponBase.activarDisparo;
                }
                else
                {
                    b8.activarB8 = false;
                }
                //With B8 they can't see you
                torso.layer = layerB8;
                ocultacion.isVisible.enabled = false;
                //Change the time that they can see you
                ocultacion.tiempoLimiteVisible = timeHiddenB8;
                ocultacion.defaultTiempoLimiteVisible = timeHiddenB8;
            }
            else
            {
                b8.gameObject.SetActive(false);
                weaponBase.gameObject.SetActive(true);

                weaponBase.activarDisparo = b8.activarB8;

                ocultacion.tiempoLimiteVisible = timeHiddenB8;
                ocultacion.defaultTiempoLimiteVisible = timeHiddenPistol;
                //torso.layer = layerB8;
            }
        }
    }
    void Icon()
    {
        if (changeWeapon)
        {
            pistolIcon.enabled = false;
            b8Icon.enabled = true;
        }
        else
        {
            pistolIcon.enabled = true;
            b8Icon.enabled = false;
        }
    }

}
