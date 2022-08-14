using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActiveGenerador : MonoBehaviour
{
    SoldadoNormal soldadoNormal;

    [SerializeField] Transform ubiGenerador;
    [SerializeField] Transform ubiOriginal;

    [SerializeField] EstadoGenerador generator;
    float timeIrGenerador;
    private void Awake()
    {
        soldadoNormal = GetComponent<SoldadoNormal>();
    }

    // Update is called once per frame
    void Update()
    {
        EncenderGenerador();
    }
    void EncenderGenerador()
    {
        if (!generator.on)
        {
            timeIrGenerador += Time.deltaTime;
            if (timeIrGenerador >= 3)
            {
                soldadoNormal.ubicacionesDirigir[0] = ubiGenerador;
            }

        }
        else 
        {
            timeIrGenerador = 0;
            soldadoNormal.ubicacionesDirigir[0] = ubiOriginal;
        }
    }
}
