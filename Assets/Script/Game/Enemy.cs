using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Disparo")]
    public Transform enemySpawnShoot;
    public GameObject bulletEnemy;
    public float tiempoEntreDisparo = 5f;
    public float tiempoActual;
    public bool reiniciarTiempo = true;
    public int numeroBalasEnemigo = 0;
    private int totalEnemigoDisparos = 1;

    [Header("Inteligencia Artificial")]
    public Transform positionPlayer;
    public float velocidadEnemy;
    public float distanciaLimite;

    [Header("Turno")]
    public ContadordeTiempo ct;
    void Start()
    {
        reiniciarTiempo = false;
        positionPlayer = GameObject.Find("Player").transform;
        tiempoActual = 0;
    }
    void Update()
    {
        if (!Turn.turnos)
        {
            StartCoroutine("TurnoEntrePersonajes");
        }
        if (Turn.turnos)
        {
            StopCoroutine("TurnoEntrePersonajes");
        }
    }
    public IEnumerator TurnoEntrePersonajes()
    {
        yield return new WaitForSeconds(3f);
        ct.TiempoRestanteEnemy();
        yield return new WaitForSeconds (5f);
        MovimientoPersonaje();
        yield return new WaitForSeconds(2f);
        TiempoEspera();
    }
    //Disparo enemigo provisional
    #region
    public void TiempoEspera()
    {
        numeroBalasEnemigo = 0;
        reiniciarTiempo = true;
        if (reiniciarTiempo)
        {
            tiempoActual += 0.01f;
            if (tiempoActual >= tiempoEntreDisparo && numeroBalasEnemigo <= totalEnemigoDisparos)
            {
                Instantiate(bulletEnemy, enemySpawnShoot.position, enemySpawnShoot.rotation);
                reiniciarTiempo = false;
                numeroBalasEnemigo++;
            }
        }
        if (!reiniciarTiempo)
        {
            tiempoActual = 0;
            

        }
    }
    #endregion
    //Movimiento del personaje
    #region
    public void MovimientoPersonaje()
    {
        if (Vector2.Distance(transform.position, positionPlayer.position) > distanciaLimite)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionPlayer.position, velocidadEnemy * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, positionPlayer.position) < distanciaLimite)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionPlayer.position, -velocidadEnemy * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, positionPlayer.position) > distanciaLimite && Vector2.Distance(transform.position, positionPlayer.position) < distanciaLimite)
        {
            transform.position = transform.position;

        }


        if (positionPlayer.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
        }
    }
    #endregion



}
