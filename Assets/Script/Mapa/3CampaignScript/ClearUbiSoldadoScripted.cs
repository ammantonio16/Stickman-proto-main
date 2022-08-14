using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUbiSoldadoScripted : MonoBehaviour
{
    public SoldadoNormal soldadoScripted;

    public Transform ubicacionDefault;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            soldadoScripted.ubicacionesDirigir.Clear();

            soldadoScripted.ubicacionesDirigir.Add(ubicacionDefault);
        }
    }
}
