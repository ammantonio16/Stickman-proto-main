﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHere : MonoBehaviour
{
    [HideInInspector] public bool playerImHere;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerImHere = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerImHere = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerImHere = false;
    }

}
