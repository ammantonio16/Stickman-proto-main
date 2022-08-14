using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientificRoomOpen : MonoBehaviour
{
    public int indexMod;
    PlayerHere player;
    private void Awake()
    {
        player = GetComponent<PlayerHere>();
    }
    void Update()
    {
        if (!StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) OpenScientificRoom();
    }
    void OpenScientificRoom()
    {
        if (player.playerImHere)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
            }
        }
    }
}
