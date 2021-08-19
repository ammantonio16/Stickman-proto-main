using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrulla : MonoBehaviour
{
    public Transform[] ubicaciones;
    public float speed;
    Rigidbody2D rbSoldier;
    Transform transformSoldier;
    public float tiempoEspera;
    public int puntosUbi = 0;
    public float limiteCambiarPunto;
    public Transform checkGround;
    float distanciaPuntos;
    Patrulla patrulla;
    void Start()
    {
        patrulla = GetComponent<Patrulla>();
        rbSoldier = GetComponent<Rigidbody2D>();
        transformSoldier = GetComponent<Transform>();
       
    }
    void Update()
    {
        ScalePersonaje();
    }
    private void FixedUpdate()
    {
        int ground = (1 << 9);
        RaycastHit2D isGround;
        isGround = Physics2D.Raycast(checkGround.position, -checkGround.up, 0.29f, ground);
        if (isGround)
        {
            if (puntosUbi < ubicaciones.Length)
            {
                distanciaPuntos = rbSoldier.position.x - ubicaciones[puntosUbi].position.x;
                //Debug.Log("La distancia" + " " + distanciaPuntos);
                Vector3 ubiSoldier = rbSoldier.position;
                ubiSoldier.x = Mathf.MoveTowards(rbSoldier.position.x, ubicaciones[puntosUbi].position.x, speed * Time.deltaTime);
                rbSoldier.MovePosition(ubiSoldier);
                if (distanciaPuntos <= 0.1 && distanciaPuntos >= -0.1)
                {
                    limiteCambiarPunto += Time.deltaTime;
                    if (limiteCambiarPunto >= tiempoEspera)
                    {
                        limiteCambiarPunto = 0;
                        puntosUbi++;
                    }
                }
            }
            if (puntosUbi >= ubicaciones.Length)
            {
                puntosUbi = 0;
            }
        }
        else
        {
            Debug.Log("No puedo patrullar porque no estoy tocando el suelo UwU");
        }
    }
    void ScalePersonaje()
    {
        if(distanciaPuntos > 0)
        {
            transformSoldier.localScale = new Vector3(-1, 1, 1);
        }
        if(distanciaPuntos < 0)
        {
            transformSoldier.localScale = new Vector3(1, 1, 1);
        }
    }

}
