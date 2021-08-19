using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLife : MonoBehaviour
{
    public float vidaZombie = 100f;
    public GameObject[] cuerpoZombie;
    public float damage;
    Rigidbody2D rbSombi;
    ZombieIA zombieIa;
    ZombieDamege dañoDelZombie;
    Animator zombieAnimation;

    void Start()
    {
        zombieAnimation = GetComponent<Animator>();
        dañoDelZombie = GetComponentInChildren<ZombieDamege>();
        zombieIa = GetComponent<ZombieIA>();
        rbSombi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DañoRecibidoZombie(damage);
    }
    public void DañoRecibidoZombie(float daño)
    {
        zombieAnimation.SetFloat("VidaZombie", vidaZombie);
        vidaZombie -= daño;
        if(vidaZombie <= 0)
        {
            zombieIa.detectarPlayer = false;
            zombieIa.detectarEnemy = false;
            Destroy(dañoDelZombie);
            gameObject.tag = "Cadaver";
            //float tiempoParaAction = Time.deltaTime;
            if(!ZombieIA.tocarGround && !zombieIa.rayoEspalda)
            {
                zombieIa.enabled = false;
                foreach (GameObject parteZombie in cuerpoZombie)
                {
                    parteZombie.GetComponent<Collider2D>().enabled = false;
                }
                Destroy(rbSombi);
            }
        }
    }
}
