using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncenderTaladro : MonoBehaviour
{
    PlayerHere herePlayer;
    [SerializeField] SoldierLife vidaSoldado;
    bool enemyHere;
    float timeToOnDrill_Enemy;
    //Quiza cambiar el static 
    public static bool encender;
    [SerializeField] List<ParticleSystem> particulasTaladro;

    int terminarParticulas;

    private void Awake()
    {
        encender = false;
        herePlayer = GetComponent<PlayerHere>();
    }
    void Update()
    {
        Encender_Taladro();
        Apagar_TaladroEnemy();
    }
    
    void Encender_Taladro()
    {
        //Si estoy aqui
        if (herePlayer.playerImHere)
        {
            //Puedo encender el generador
            if (Input.GetKeyDown(KeyCode.Z))
            {
                encender = !encender;
            }
        }
        //Si lo enciendo salen las particulas
        if (encender)
        {
            if(terminarParticulas < 1)
            {
                for (int i = 0; i < particulasTaladro.Count; i++)
                {
                    Debug.Log("Permutar" + i);
                    particulasTaladro[i].Play();
                    if (i >= particulasTaladro.Count - 1) terminarParticulas++;
                }
            }
        }
        //Sino se paran las particulas
        else
        {
            if(terminarParticulas > 0)
            {
                for (int w = 0; w < particulasTaladro.Count; w++)
                {
                    Debug.Log("No Permutar" + w);
                    particulasTaladro[w].Stop();
                    if (w >= particulasTaladro.Count - 1) terminarParticulas--;
                }
            }
        }

    }
    void Apagar_TaladroEnemy()
    {
        if (enemyHere)
        {
            if(vidaSoldado.vida > 0)
            {
                timeToOnDrill_Enemy += Time.deltaTime;
                if (timeToOnDrill_Enemy >= 1)
                {
                    encender = false;
                }
            }

        }
        else
        {
            timeToOnDrill_Enemy = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHere = false;
        }
    }

}
