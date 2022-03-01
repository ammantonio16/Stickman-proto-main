﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValorArbolesPuzle : MonoBehaviour
{
    public GameObject botonInteractuar;
    

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
            botonInteractuar.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            botonInteractuar.SetActive(false);
        }
    }

}
