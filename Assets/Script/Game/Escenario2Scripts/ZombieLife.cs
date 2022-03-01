using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieLife : MonoBehaviour
{
    public float vidaZombie;
    public float maxVidaZombie;
    public Image barraVidaSombi;
    public GameObject[] cuerpoZombie;
    Rigidbody2D rbSombi;
    ZombieIA zombieIa;
    Enemigo enemyIa;
    ZombieDamege dañoDelZombie;
    Animator zombieAnimation;
    float basaka = 0;

    public bool enemigo;
    public bool sombi;

    float timeDestroyRb;

    public bool checkMuerte;

    void Start()
    {
        zombieAnimation = GetComponent<Animator>();
        dañoDelZombie = GetComponentInChildren<ZombieDamege>();
        if (sombi)
        {
            zombieIa = GetComponent<ZombieIA>();
        }
        if (enemigo)
        {
            enemyIa = GetComponent<Enemigo>();
        }
        rbSombi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        barraVidaSombi.fillAmount = vidaZombie / maxVidaZombie;
        if (sombi)
        {
            DañoRecibidoZombie(basaka);
        }
        if (enemigo)
        {
            DañoRecibidoEnemy(basaka);
        }
        

    }
    public void DañoRecibidoZombie(float daño)
    {
        zombieAnimation.SetFloat("VidaZombie", vidaZombie);
        vidaZombie -= daño;
        if(vidaZombie <= 0)
        {
            
            gameObject.layer = 0;
            zombieIa.detectarPlayer = false;
            zombieIa.detectarEnemy = false;
            Destroy(dañoDelZombie);
            gameObject.tag = "Cadaver";
            if (!ZombieIA.tocarGround)
            {
                foreach (GameObject parteZombie in cuerpoZombie)
                {
                    parteZombie.layer = 0;
                }
                
            }
            if (checkMuerte)
            {
                Destroy(rbSombi);
            }
        }
    }
    public void DañoRecibidoEnemy(float daño)
    {
        zombieAnimation.SetFloat("VidaEnemy", vidaZombie);
        vidaZombie -= daño;
        if (vidaZombie <= 0)
        {
            gameObject.layer = 0;
            enemyIa.detectarPlayer = false;
            enemyIa.detectarSombi = false;
            gameObject.tag = "Cadaver";
            foreach (GameObject parteEnemy in cuerpoZombie)
            {
                parteEnemy.tag = "Cadaver";
                parteEnemy.layer = 0;
            }
            if (checkMuerte)
            {
                Destroy(rbSombi);
            }
            
            
        }
        
    }
    
    public void muerte()
    {
        if (vidaZombie <= 0){

        }
    }
}
