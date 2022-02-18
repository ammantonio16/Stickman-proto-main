using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float tiempo = 4f;
    public float caer;
    public bool deteccion;

    private void Start()
    {
        caer = 0f;
    }
    private void Update()
    {
        if (deteccion == true)
        {
            caer += Time.deltaTime;

            if (caer >= tiempo)
            {
                caer = 0f;
                rb.bodyType = RigidbodyType2D.Dynamic;
                caer = 0f;
                deteccion = false;
            }
        } 
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //anim.enabled = (true);
            deteccion = true;
        }
    }
}
