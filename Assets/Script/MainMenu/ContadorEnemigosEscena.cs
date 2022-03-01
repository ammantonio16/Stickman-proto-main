using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorEnemigosEscena : MonoBehaviour
{
    public int numeroEnemigos;
    public GameObject[] enemigos;
    public Animator ganar;
    public Canvas pantallaGanar;
    public GameObject nextLevel;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemigos)
        {
            if (numeroEnemigos < enemigos.Length)
            {
                numeroEnemigos++;
            }
        }
        if (numeroEnemigos > enemigos.Length)
        {
            numeroEnemigos--;
            
        }
        if(numeroEnemigos <= 0)
        {
            nextLevel.SetActive(true);
            ganar.SetBool("Ganar", true);
        }
    }
}
