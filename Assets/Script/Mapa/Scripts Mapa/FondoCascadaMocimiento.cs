using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoCascadaMocimiento : MonoBehaviour
{
    public float speed;
    public Transform cascada;
    public bool mov;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoverFondo();
    }
    void MoverFondo()
    {
        if (mov)
        {
            cascada.Translate(new Vector2(0f, speed * Time.time));
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            mov = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            mov = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            mov = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            mov = true;
        }
    }
}
