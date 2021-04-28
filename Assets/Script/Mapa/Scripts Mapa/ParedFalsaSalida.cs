using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedFalsaSalida : MonoBehaviour
{
    public SpriteRenderer[] paredFalsa;
    public Color grisPared;
 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            paredFalsa[0].color = grisPared;
            paredFalsa[1].color = grisPared;
        }
    }
}
