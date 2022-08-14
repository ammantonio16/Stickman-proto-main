using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestWeapon : MonoBehaviour
{
    [SerializeField] GameObject bestWeapon;
    [SerializeField] ArmaController weaponPlayer;
    void Start()
    {
        //Spawn Besto Weapon B8
        if(weaponPlayer.municionTotal <= 0 && !SaveScene.instancia.b8)
        {
            bestWeapon.SetActive(true);
        }
    }
}
