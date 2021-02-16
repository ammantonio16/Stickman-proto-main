using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turn : MonoBehaviour
{
    public float contador;
    public GameObject player;
    public GameObject enemy;

    public static bool seguirBala = false;
    public static bool turnos = true;
    public static bool moverCamera = true;
    public static bool direccionbala = true;
    public static bool direccionbalaEnemy = true;
}
