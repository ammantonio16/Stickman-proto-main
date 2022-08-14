using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContarEnemigos : MonoBehaviour
{
    [SerializeField]
    public static int numeroEnemigos = 0;
    public LifePlayer[] enemy;

    public Enemigo[] soldiers;
    public int numSoldados;

    private void Start()
    {
        soldiers = GameObject.FindObjectsOfType<Enemigo>();
        ContarSoldier();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Sumame" + numeroEnemigos); 
            numeroEnemigos = numeroEnemigos + 1;
            //enemy = new GameObject[numeroEnemigos];
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            numeroEnemigos = numeroEnemigos - 1;
            Debug.Log("Restame" + numeroEnemigos);
            //enemy = new GameObject[numeroEnemigos];
            //enemy = GameObject.FindObjectsOfType(typeof(Life)) as GameObject[];
        }
    }
    void ContarSoldier()
    {
        foreach(Enemigo soldier in soldiers)
        {
            numSoldados++;
        }
    }
}
