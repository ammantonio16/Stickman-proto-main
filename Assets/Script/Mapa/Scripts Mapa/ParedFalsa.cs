using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedFalsa : MonoBehaviour
{
    public SpriteRenderer trasparencia;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
             trasparencia.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
