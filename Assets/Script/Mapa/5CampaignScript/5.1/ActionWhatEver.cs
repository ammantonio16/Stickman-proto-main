using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWhatEver : MonoBehaviour
{
    public GameObject fan;
    public Events evento;
    PlayerHere player;
    public bool actionEvent;

    float time;
    
    private void Awake()
    {
        player = GetComponent<PlayerHere>();
    }
    
    // Update is called once per frame
    void Update()
    {
        RealizeAction();
    }

    void RealizeAction()
    {
        if (player.playerImHere)
        {
            evento.actionOnCinematic += PruebaDesapear;
            if (Input.GetKeyDown(KeyCode.Z)) 
            {
                actionEvent = true;
                Debug.Log(this.gameObject.name);
            }
            
        }
        else evento.actionOnCinematic -= PruebaDesapear;
        if (actionEvent)
        {
            evento.Cinematic(ref actionEvent);
        }
    }
    void PruebaDesapear()
    {
        fan.SetActive(false);
    }
}
