using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaZombie : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 80.15f;
    float tiempoDestruir;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * force, ForceMode2D.Impulse);
    }

    void Update()
    {
        tiempoDestruir += Time.deltaTime;
        //Debug.Log("La bala lleva viva " + " " + tiempoDestruir + " " + " segundos");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tiempoDestruir >= 0.09f)
        {
            Destroy(this.gameObject);
            tiempoDestruir = 0;
        }
        //Destroy(this.gameObject);
    }
}
