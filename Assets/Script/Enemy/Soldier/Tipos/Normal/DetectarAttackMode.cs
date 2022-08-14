using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarAttackMode : MonoBehaviour
{
    SoldadoNormal soldado;

    private void Awake()
    {
        soldado = GetComponentInParent<SoldadoNormal>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (soldado.berserker) { Debug.Log("Player esta Dentro"); soldado.seePlayer = true; }
            
        }
        if (collision.gameObject.CompareTag("BalaPlayer"))
        {
            soldado.seePlayer = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (soldado.berserker) { Debug.Log("Player esta Dentro"); soldado.seePlayer = true; }

        }
        if (collision.gameObject.CompareTag("BalaPlayer"))
        {
            soldado.seePlayer = true;
        }
    }
}
