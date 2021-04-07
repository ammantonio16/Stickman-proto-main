using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    public GameObject explosion;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Electricidad");
            foreach(GameObject target in gameObjects)
            {
                GameObject.Destroy(target);
                Destroy(gameObject);
            }
        }
    }
}
