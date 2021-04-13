using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionMuerte : MonoBehaviour
{
    public Animator transicionMuerte;
    public Life vidaPlayer;
    public Canvas repetir;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(vidaPlayer.actualLife <= 0)
        {
            transicionMuerte.SetFloat("VidaPlayer", vidaPlayer.actualLife);
            repetir.SetActive(true);
        }
    }
}
