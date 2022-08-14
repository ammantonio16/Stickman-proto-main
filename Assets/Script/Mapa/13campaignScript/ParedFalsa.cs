using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedFalsa : MonoBehaviour
{
    public SpriteRenderer[] trasparencia;
    public float r, g, b, a;
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
             trasparencia[0].color = new Color(r, g, b, a);
             trasparencia[1].color = new Color(r, g, b, a);
        }
    }
}
