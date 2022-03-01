using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCajas : MonoBehaviour
{
    public float tiempoSpawnCaja;
    public float actualTiempo;
    public GameObject cajaPrefab;
    public Transform spawnCaja;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        actualTiempo += Time.deltaTime;
        if (actualTiempo >= tiempoSpawnCaja)
        {
            Instantiate(cajaPrefab, spawnCaja.position, Quaternion.identity);
            actualTiempo = 0;

        }

    }

}
