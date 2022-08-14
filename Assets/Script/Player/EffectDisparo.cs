using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisparo : MonoBehaviour
{
    public GameObject brazoRotation;
    public ParticleSystem effectoDisparoParticle;
    bool encender;
    public Transform playerScale;
    public GameObject luzFake;
    void Start()
    {

        //effectDisparo.main.startRotation = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (encender)
        {
            AnimationEffectDisparo();
            encender = false;
        }
    }
    public void AnimationEffectDisparo()
    {
        effectoDisparoParticle.Play();
        GameObject luzClone = Instantiate(luzFake, effectoDisparoParticle.transform.position, Quaternion.identity);
        Destroy(luzClone, 0.1f);
        if (playerScale.localScale.x > 0)
        {
            effectoDisparoParticle.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        if(playerScale.localScale.x < 0)
        {
            effectoDisparoParticle.transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
        }
        //effectoDisparoParticle.Play();
        
    }
}
