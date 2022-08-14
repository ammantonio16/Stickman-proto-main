using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAmmo : MonoBehaviour
{
    PlayerHere player;
    [SerializeField] ArmaController playerWeapon;
    public int ammoRecovery;
    public int indexAmmo;
    private void Awake()
    {
        player = GetComponent<PlayerHere>();
    }
    void Start()
    {
        if (AmmoRecoveryList.ammoFromLevel.ammo[indexAmmo]) this.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        UpAmmo();
    }
    void UpAmmo()
    {
        if (player.playerImHere)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                playerWeapon.municionTotal += ammoRecovery;
                this.gameObject.SetActive(false);
                AmmoRecoveryList.ammoFromLevel.ammo[indexAmmo] = true;
            }
        }
    }
}
