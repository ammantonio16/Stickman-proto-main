using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCampaign10 : MonoBehaviour
{
    public Collider2D switchButton;
    public ascensorCampaign10 ascensor;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {
            ascensorCampaign10.time = 0;
            ascensor.enabled = true;
        }
    }
}
