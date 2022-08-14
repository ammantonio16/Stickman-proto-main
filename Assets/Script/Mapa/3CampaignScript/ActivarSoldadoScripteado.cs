using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarSoldadoScripteado : MonoBehaviour
{
    public SoldadoNormal soldadoScripted;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Escondite")) soldadoScripted.enabled = true;
        if (collision.gameObject.CompareTag("Enemy")) { soldadoScripted.maxTiempoUbi = 10; soldadoScripted.siguienteUbi = 0; }
    }
    
}
