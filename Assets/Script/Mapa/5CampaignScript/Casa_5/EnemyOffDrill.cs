using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOffDrill : MonoBehaviour
{
    SoldadoNormal soldadoVigiaGenerador;

    [SerializeField] Transform ubiGenerador;
    [SerializeField] Transform ubiOriginal;

    float timeToOffDrill;

    // Update is called once per frame
    private void Awake()
    {
        soldadoVigiaGenerador = GetComponent<SoldadoNormal>();
    }
    void Update()
    {
        OffDrill();
    }
    void OffDrill()
    {
        if (EncenderTaladro.encender)
        {
            timeToOffDrill += Time.deltaTime;
            if (timeToOffDrill >= 1)
            {
                soldadoVigiaGenerador.ubicacionesDirigir[0] = ubiGenerador;
            }

        }
        else
        {
            timeToOffDrill = 0;
            soldadoVigiaGenerador.ubicacionesDirigir[0] = ubiOriginal;
        }
    }
}
