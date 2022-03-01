using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KynematicADinamyc : MonoBehaviour
{
    GameObject armaRedonda;
    void Start()
    {
        armaRedonda = GameObject.FindGameObjectWithTag("ArmaRedonda");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ArmaRedonda")
        {
            armaRedonda.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
