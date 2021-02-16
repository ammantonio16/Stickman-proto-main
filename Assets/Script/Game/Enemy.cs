using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Disparo")]
    public Transform enemySpawnShoot;
    public GameObject bulletEnemy;
    public float tiempoEntreDisparo = 3f;
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

    public BulletEnemy bal;

    public WeaponController mb;
    [Header("Características hereditarias")]
    public GameObject objetoPadre;
    public GameObject objetoHijo;
    public ArrowMovement fc;


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
            objetoHijo = Instantiate(fc.arrow, fc.spawn.position, fc.spawn.rotation);
            objetoHijo.transform.parent = objetoPadre.transform;
            objetoHijo.transform.position = objetoPadre.transform.position;
            mb.numeroDisparos = 0;
            StartCoroutine("TurnoEntrePersonajes");
        }
        if (Turn.turnos)
        {
            StopCoroutine("TurnoEntrePersonajes");
            numeroBalasEnemigo = 0;
        }
    }
    public IEnumerator TurnoEntrePersonajes()
    {
        yield return new WaitForSeconds(3f);
        ct.TiempoRestanteEnemy();
        yield return new WaitForSeconds(1f);
        MovimientoPersonaje();
        yield return new WaitForSeconds(3f);
        TiempoEspera();
    }
    //Disparo enemigo provisional
    #region
    public void TiempoEspera()
    {
           if (numeroBalasEnemigo < totalEnemigoDisparos)
            {
                Instantiate(bulletEnemy, enemySpawnShoot.position, enemySpawnShoot.rotation);
                bal.rangobala = new Vector2(Random.Range(bal.rangoBala2.position.x, bal.rangoBala1.position.x), bal.rangoBala3.position.y);
                reiniciarTiempo = false;
                numeroBalasEnemigo++;
                Debug.Log(bal.rangobala);
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
            Turn.direccionbalaEnemy = false;
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
            Turn.direccionbalaEnemy = true;
        }
    }
    #endregion



}
