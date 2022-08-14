﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dardo : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  Destroy(this.gameObject);

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
    
}