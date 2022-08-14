using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePlayer : MonoBehaviour
{
    public float actualLife;
    public float maxLife;
    public Image barraDeVida;
    public CambiarEscalaConMira centroPersonaje;
    JugadorMovimiento jugadorMov;
    EscaleraMano escaleraMano;
    Animator player;

    SoundManager soundManager;
    private void Start()
    {
        player = GetComponent<Animator>();
        escaleraMano = GetComponent<EscaleraMano>();
        jugadorMov = GetComponent<JugadorMovimiento>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    void Update()
    {
        barraDeVida.fillAmount = actualLife / maxLife;
    }

    public void VidaBaja(float daño)
    {

        actualLife = actualLife - daño;
        if (actualLife <= 0)
        {
            Debug.Log("Te moriste");
            
            player.SetBool("Dead", true);
            jugadorMov.enabled = false;
            escaleraMano.enabled = false;
        }
    }
}
