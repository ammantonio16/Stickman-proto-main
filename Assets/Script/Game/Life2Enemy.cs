using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life2Enemy : MonoBehaviour
{
    public float actualLife;
    public float maxLife;
    public Animator player;
    public CambiarEscalaConMira centroPersonaje;
    void Update()
    {
        VidaBaja(maxLife);
    }

    public void VidaBaja(float daño)
    {

        actualLife = actualLife - daño;
        if (actualLife <= 0)
        {
            centroPersonaje.SetActive(false);
            player.SetBool("Dead", true);

        }
    }
}
