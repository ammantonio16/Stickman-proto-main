using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoPrueba : MonoBehaviour
{
    public ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    //Asignar en el Trigger de Particulas de FireChildBase el collider del Player

    void OnParticleTrigger()
    {

        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            //SJugadorMovimiento.quemar = true;
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(0, 255, 0, 255);
            enter[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(255, 0, 0, 255);
            exit[i] = p;
        }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        

    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Sombis")
        {
            other.gameObject.GetComponentInParent<ZombieIA>().quemarSombi = true;
        }
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<Enemigo>().quemarSoldier = true;
        }
       
    }

}
