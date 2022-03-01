﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [Header("Características disparo")]
    public float speed;
    public float range;
    public Transform arrowPosition;
    Rigidbody2D rb; 
    bool drag = true;
    Vector3 dis;
    SpriteRenderer sprite;

    [Header("Spawn del bitmap")]
    public GameObject arrow;
    public Transform spawn;

    [Header("Características hereditarias")]
    public GameObject objetoPadre;
    public GameObject objetoHijo;

    public GameObject trajectoryDot;
    public GameObject[] trajectoryDots;
    public int number;
    Vector3 force;
    Vector3 pos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        sprite = GetComponent<SpriteRenderer>();
        trajectoryDots = new GameObject[number];
        /*sprite.enabled = false;*/
    }

    //Detecta cuando el jugador arrastra su dedo por la pantalla
    private void OnMouseDown()
    {
        for (int i = 0; i < number; i++)
        {
            trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
        }
    }
    void OnMouseDrag()
    {
        /*sprite.enabled = false;*/
        if (!drag)
            return;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dis = pos - arrowPosition.position;
        dis.z = 0;

        if (dis.magnitude > range)
        {
            dis.z = 0;
            dis = dis.normalized * range;
        }
        for (int i = 0; i < number; i++)
        {
            trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
        }


    }
    //Detecta cuando el jugador suelta el dedo de la pantalla
    void OnMouseUp()
    {
        /*sprite.enabled = true; */
        if (!drag)
            return;
        drag = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        force = -dis.normalized * speed * dis.magnitude * speed / range;
        rb.AddForce(force, ForceMode2D.Impulse);
        for (int i = 0; i < number; i++)
        {
            Destroy(trajectoryDots[i]);
        }

    }
    //Trigger que detecta con que colisiona el bitmap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            objetoHijo = Instantiate(arrow, spawn.position, spawn.rotation);
            objetoHijo.transform.parent = objetoPadre.transform;
            objetoHijo.transform.position = objetoPadre.transform.position;
            Destroy(this.gameObject);
            Turn.turnos = false;
        }

        if (collision.gameObject.tag == "Enemy"){
            objetoHijo = Instantiate(arrow, spawn.position, spawn.rotation);
            objetoHijo.transform.parent = objetoPadre.transform;
            objetoHijo.transform.position = objetoPadre.transform.position;
            
            Destroy(this.gameObject);
            Turn.turnos = false;
        }

        if (collision.tag == "Enemy" || collision.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;
        }
    }

    private Vector2 calculatePosition(float elapsedTime)
    {
        return new Vector2(pos.x, pos.y) +
            new Vector2(-dis.x * speed, -dis.y * speed) * elapsedTime +
            0.5f * Physics2D.gravity * elapsedTime * elapsedTime;
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            objetoHijo = Instantiate(arrow, spawn.position, spawn.rotation);
            objetoHijo.transform.parent = objetoPadre.transform;
            objetoHijo.transform.position = objetoPadre.transform.position;
            Destroy(this.gameObject);
            Turn.turnos = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            objetoHijo = Instantiate(arrow, spawn.position, spawn.rotation);
            objetoHijo.transform.parent = objetoPadre.transform;
            objetoHijo.transform.position = objetoPadre.transform.position;
            collision.GetComponent<Life>().VidaBaja(10);
            Destroy(this.gameObject);
            Turn.turnos = false;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground")
        {
            ContadordeTiempo.tiempoAcabarTurno = 20f;
        }
    }*/

}

