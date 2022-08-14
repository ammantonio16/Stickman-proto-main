using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zonas_Checkpoint : MonoBehaviour
{
    public int checkPointActual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) CheckPointController.numeroCheckPoint = checkPointActual;
                                                      
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) CheckPointController.numeroCheckPoint = checkPointActual;
    }
}
