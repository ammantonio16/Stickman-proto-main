using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLights : MonoBehaviour
{
    public Collider2D switchButton;
    public GameObject panel;
    public Ascensor ascensor;
    public Ascensor ascensor1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {
            ascensor.enabled = true;
            ascensor1.enabled = true;
            panel.SetActive (false); 
        }
    }
}
