using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaDardos : MonoBehaviour
{
    [SerializeField] GameObject dardo;

    [SerializeField] Transform spawnDardo;

    float tiempoSpawnDardo;
    public float tiempoSpawnDardoLimite;
    public float velocidadDardo;

    [SerializeField] bool rapido;
    void Update()
    {
        if (SaveScene.instancia.activarTrampasTemplo)
        {
            DispararDardo();
        }
    }

    void DispararDardo()
    {
        tiempoSpawnDardo += Time.deltaTime;
        if(tiempoSpawnDardo >= tiempoSpawnDardoLimite)
        {
            GameObject dardoClone = Instantiate(dardo, spawnDardo.position, Quaternion.Euler(new Vector3(0,0,-90f)));
            if(rapido) velocidadDardo = Random.Range(15, 20);
            else velocidadDardo = Random.Range(9,10);
            dardoClone.GetComponent<Rigidbody2D>().AddForce(transform.right * velocidadDardo, ForceMode2D.Impulse);
            tiempoSpawnDardo = 0;
        }
    }
}
