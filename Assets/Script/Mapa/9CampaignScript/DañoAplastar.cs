using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoAplastar : MonoBehaviour
{
     public float daño;

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.tag == "Player")
        {
            GameObject playerLife = GameObject.FindGameObjectWithTag("Player");
            playerLife.GetComponent<LifePlayer>();
        }
    }
}
