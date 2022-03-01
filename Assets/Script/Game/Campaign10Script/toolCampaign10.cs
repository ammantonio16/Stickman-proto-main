using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolCampaign10 : MonoBehaviour
{
    //public DoorNextLevel door;
    public GameObject player;
    public Animator animacion;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            animacion.enabled = true;
        }
    }
}
