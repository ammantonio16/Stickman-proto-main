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

    //El bool enemigo se colocara en true manualmente si quien lleva el script es un enemigo
    //probar la fuerza salto en la otra orientación para comprobar si funciona el salto o habría que cambiarlo a negativo cuando la escala cambie
    public bool enemigo;
    public Enemigo enemigoIa;

    public bool patrullaActivada;
    float tiempoActivePA;
    //Colocar true solo a los enemigo de manera manual
    public bool soyEnemigo;

    
    void Start()
    {
        patrulla = GetComponent<Patrulla>();
        rbSoldier = GetComponent<Rigidbody2D>();
        transformSoldier = GetComponent<Transform>();
        if (enemigo)
        {
            enemigoIa = GetComponent<Enemigo>();
        }
        
    }
    void Update()
    {
        tiempoActivePA += Time.deltaTime;
        if (soyEnemigo)
        {
            if (tiempoActivePA >= 5)
            {
                patrullaActivada = true;
                enemigoIa.enabled = true;
            }
        }
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
                if (enemigo)
                {
                    int bitmask = (1 << 9);
                    RaycastHit2D checkSalto;
                    checkSalto = Physics2D.Raycast(enemigoIa.checksalto.position, Vector2.right, enemigoIa.orientacionRayoSalto, bitmask);
                    RaycastHit2D checkPared;
                    checkPared = Physics2D.Raycast(enemigoIa.checkpared.position, Vector2.right, enemigoIa.orientacionRayoSalto, bitmask);
                    if (checkSalto)
                    {
                        Debug.Log("Puedo saltar");
                        rbSoldier.AddForce(new Vector2(enemigoIa.orientacionSalto, enemigoIa.fuerzaSalto), ForceMode2D.Impulse);
                        if (checkPared)
                        {
                            //Debug.DrawRay(checkpared.position, new Vector2(orientacionRayoSalto, 0f), Color.white);
                            Vector3 stopPosition = transform.position;
                            rbSoldier.MovePosition(stopPosition);
                        }
                    }
                    if (!checkSalto)
                    {
                        distanciaPuntos = rbSoldier.position.x - ubicaciones[puntosUbi].position.x;
                        //Debug.Log("La distancia" + " " + distanciaPuntos);
                        Vector3 ubiSoldier2 = rbSoldier.position;
                        ubiSoldier2.x = Mathf.MoveTowards(rbSoldier.position.x, ubicaciones[puntosUbi].position.x, speed * Time.deltaTime);
                        rbSoldier.MovePosition(ubiSoldier2);

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
                }
                if (!enemigo)
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

    public void ScalePersonaje()
    {
        if(distanciaPuntos > 0)
        {
            //enemigoIa.orientacionRayoSalto = -0.55f;
            transformSoldier.localScale = new Vector3(-1, 1, 1);
        }
        if(distanciaPuntos < 0)
        {
            //enemigoIa.orientacionRayoSalto = 0.55f;
            transformSoldier.localScale = new Vector3(1, 1, 1);
        }
    }

}
