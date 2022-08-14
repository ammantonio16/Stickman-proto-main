using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierLife : MonoBehaviour
{
    //Soldado normal 1;
    //Soldado Armadura 3;
    [Header("Vida Soldado Data")]
    public int vida;
    ActualStatus status;


    [Header("Animacion Dormir")]
    public ParticleSystem particularDormir;
    Animator anim;

    SoldadoNormal soldado;
    int restarSoldados;

    [Header("CambiarLayer")]
    [SerializeField] LayerMask dormido;
    [SerializeField]GameObject cuerpo;

    private void Awake()
    {
        //Tipo de enemigo
        soldado = GetComponent<SoldadoNormal>();

        //Particulas de sueño
        particularDormir = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();

        status = GetComponent<ActualStatus>();
    }
    void Start()
    {
        anim.SetLayerWeight(1, 0);
        restarSoldados = 0;

        //Status Animacion
        anim.SetInteger("Status Life", vida);
    }

    
    void Update()
    {

        if(vida <= 0)
        {

            anim.SetLayerWeight(1, 1);
            //Anula el estado de uno de los dos tipos de enemigos

            RestarSoldadoActivo();
            soldado.contorno.SetActive(false);

            //Activar animacion de noqueo
            anim.SetBool("Sleeping", true);

            //Tras haber quedado dormmido tu layer cambia
            cuerpo.layer = 12;

        }
        

    }
    public void Daño(int daño)
    {
        vida -= daño;
        status.indexCasco -= daño;
        //cabeza.sprite = casco[indexCasco];
        //indexCasco = indexCasco - daño;
    }
    public void SleepingParticles()
    {
        particularDormir.Play();
    }
    void RestarSoldadoActivo()
    {
        if (restarSoldados < 1)
        {
            if (soldado.berserker)
            {
                soldado.berserker = false;
                soldado.enabled = false;
            }
            else { soldado.enabled = false; }
            restarSoldados++;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ObjetoCaer"))
        {
            Daño(3);
            Destroy(collision.gameObject);
            
        }
    }



}
