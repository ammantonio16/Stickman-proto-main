using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomperCuerda : MonoBehaviour
{
    Rigidbody2D rbChild;
    void Start()
    {
        rbChild = this.transform.GetChild(0).GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaPlayer")) rbChild.bodyType = RigidbodyType2D.Dynamic;
    }
}
