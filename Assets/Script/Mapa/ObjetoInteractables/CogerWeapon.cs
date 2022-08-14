using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerWeapon : MonoBehaviour
{
    PlayerHere player;
    private void Awake()
    {
        player = GetComponent<PlayerHere>();
    }
    void Start()
    {
        if (SaveScene.instancia.b8) this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.playerImHere) PickWeapon();
    }
    void PickWeapon()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveScene.instancia.b8 = true;
            SaveScene.instancia.b8Reogida++;
            this.gameObject.SetActive(false);
        }
    }
}
