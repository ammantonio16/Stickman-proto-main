using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turn : MonoBehaviour
{
    // Start is called before the first frame update
    public float contador;
    public GameObject player;
    public GameObject enemy;

    public static bool seguirBala = false;

    public static bool turnos = true;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
    }
  
    /*IEnumerable TurnoEntrePersonajes(float tiempo, Action accion)
    {
        yield return new WaitForSeconds(tiempo);
        accion();
    }*/
}
