using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLuvia : MonoBehaviour
{
    public float spawnGotas;
    public GameObject gota;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = new Vector2(Random.Range(-9, 18f), Random.Range(5.56f, 6));
        Instantiate(gota, position, gota.transform.rotation);
        spawnGotas += Time.deltaTime;
        if(spawnGotas >= 3)
        {
            spawnGotas = 0;
        }
    }
}
