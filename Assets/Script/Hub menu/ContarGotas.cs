using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContarGotas : MonoBehaviour
{
    public int contarGotas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gota")
        {
            contarGotas++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gota")
        {
            contarGotas--;
        }
    }
}
