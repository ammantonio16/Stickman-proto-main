using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDrillScene : MonoBehaviour
{
    public bool encenderScene;
    bool startEvento;
    PlayerHere player;
    public int indexMod;
    [SerializeField] Events evento;
    [SerializeField] ParticleSystem drillEffect;
    [SerializeField] SpriteRenderer buttonGenerator;
    [SerializeField] GameObject fakeGround;

    int drillEffectUse;
    private void Awake()
    {
        player = GetComponent<PlayerHere>();
    }
    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) DeleteGroundFake();
    }

    // Update is called once per frame
    void Update()
    {
        DigWithDrill();
        if (!StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) ActiveDrill();
        ExecuteDigWithDrill();
    }
    void ActiveDrill()
    {
        if (player.playerImHere)
        {
            evento.actionOnCinematic += DeleteGroundFake;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                encenderScene = true;
                startEvento = true;

            }
        }
    }
    void ExecuteDigWithDrill()
    {
        if (startEvento) evento.Cinematic(ref startEvento);
    }
    void DigWithDrill()
    {
        if (encenderScene)
        {
            if(drillEffectUse < 1)
            {
                buttonGenerator.color = Color.green;
                drillEffect.Play();
                drillEffectUse++;
            }
        }
        else 
        {
            if(drillEffectUse > 0)
            {
                buttonGenerator.color = Color.red;
                drillEffect.Stop();
                drillEffectUse = 0;
            }
        }
    }
    void DeleteGroundFake()
    {
        fakeGround.SetActive(false);
        encenderScene = false;
        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
        //this.enabled = false;
    }
}
