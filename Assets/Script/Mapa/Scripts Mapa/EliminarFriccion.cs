using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminarFriccion : MonoBehaviour
{
    Collider2D material;

    private void Start()
    {
        material = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            material.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Friccion");
        }
    }
}
