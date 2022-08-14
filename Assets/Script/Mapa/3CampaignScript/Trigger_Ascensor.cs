using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Ascensor : MonoBehaviour
{
    public Polipasto_Normal ascensor;
    public SoldadoNormal soldadoScripteado;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) ascensor.enabled = true;
        if (collision.gameObject.CompareTag("Enemy")) soldadoScripteado.maxTiempoUbi = 5;

    }
}
