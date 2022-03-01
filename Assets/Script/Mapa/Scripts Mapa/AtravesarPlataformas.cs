using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtravesarPlataformas : MonoBehaviour
{
    public BoxCollider2D boxTrigger;
    public BoxCollider2D boxCollision;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            boxCollision.enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boxCollision.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boxCollision.enabled = true;
            
        }
    }

}
