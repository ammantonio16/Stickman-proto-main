using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posion_Gas : MonoBehaviour
{
    [SerializeField] VidaPlayer player;
    [SerializeField] PutGasMask playerPutMask;
    public int indexMod;
    
    PlayerHere playerHere;
    private void Awake()
    {
        playerHere = GetComponent<PlayerHere>();
    }
    
    void Update()
    {
        GasPoisonDamage();
    }
    void GasPoisonDamage()
    {
        if(playerHere.playerImHere && !playerPutMask.putMask)
        {
            player.Daño(0.5f);
        }
    }
}
