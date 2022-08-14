using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despertar_StrongSound : MonoBehaviour
{
    Somnolencia somnolencia;
    [SerializeField] ParticleSystem particulasDormido;
    SoldadoNormal soldadoDormido;

    [SerializeField] Transform ubiSwitch;
    [SerializeField] Transform ubiOriginal;

    float timeToGoSwitch;
    bool returnSleep;

    private void Awake()
    {
        soldadoDormido = GetComponent<SoldadoNormal>();
        somnolencia = GetComponent<Somnolencia>();
    }

    // Update is called once per frame
    void Update()
    {
        DespertarPorSonido();
    }
    void DespertarPorSonido()
    {
        if (EncenderTaladro.encender)
        {
            //Wake Up
            somnolencia.enabled = false;
            particulasDormido.Stop();
            //Change your direction guide you towards switch 
            soldadoDormido.ubicacionesDirigir[0] = ubiSwitch;

            //Wait and check if anyone off the drills
            timeToGoSwitch += Time.deltaTime;
            //if time exceed this soldier go to off
            //NOTA: Enlazar bien el tiempo con el personaje de arriba
            if(timeToGoSwitch >= 4)
            {
                //Go to off switch
                soldadoDormido.enabled = true;
                returnSleep = false;
            }
        }
        else
        {
            soldadoDormido.ubicacionesDirigir[0] = ubiOriginal;

            
            //if you are between this range, begin the time to return sleep
           if (soldadoDormido.transform.position.x <= soldadoDormido.ubicacionesDirigir[0].position.x + 0.3f && soldadoDormido.transform.position.x >= soldadoDormido.ubicacionesDirigir[0].position.x - 0.3f)
            {
                if (!returnSleep)
                {
                    timeToGoSwitch -= Time.deltaTime;
                    if (timeToGoSwitch <= 0)
                    {
                        soldadoDormido.enabled = false;
                        somnolencia.enabled = true;
                        soldadoDormido.GetComponent<Animator>().SetBool("WalkSoldier", false);
                        returnSleep = true;
                    }
                }
            }
            
        }
        

    }
}
