using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionMuerte : MonoBehaviour
{
    public Animator transicionMuerte;
    public PlayerLife vidaPlayer;
    public Canvas repetir;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(vidaPlayer.health <= 0)
        {
            transicionMuerte.SetFloat("VidaPlayer", vidaPlayer.health);
            repetir.SetActive(true);
        }
    }
}
