using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArbol : MonoBehaviour
{
    public GameObject arbol_1;
    public Transform spawnArbol;
    public int numeroArbol = 0;
    public int TotalArbol = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(numeroArbol < TotalArbol)
            {
                Instantiate(arbol_1, spawnArbol);
                numeroArbol++;
            }
            
        }
    }
}
