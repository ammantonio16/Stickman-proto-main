using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByeB8 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("B8")) 
        {
            Debug.Log("Hey you B8");
            SaveScene.instancia.b8 = false;
        }
    }
}
