using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnePanel : MonoBehaviour
{
    PanelElevator myPanel;
    PlayerHere player;
    private void Awake()
    {
        myPanel = GetComponent<PanelElevator>();
        player = GetComponent<PlayerHere>();
    }

    // Update is called once per frame
    void Update()
    {
        CanUsePanel();
    }
    void CanUsePanel()
    { 
        //Solo funciona uno
        if (player.playerImHere)
        {
            myPanel.enabled = true;
        }
        else myPanel.enabled = false;
    }
}
