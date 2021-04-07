using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PruebaSuelo : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Física")]
    public float velocidad = 200f;
    public float nextWaypointDistance = 3f;
    public float nodeTamañoSaltoRequerido = 0.8f;
    public float modificadorSalto = 0.3f;
    public float controladorSalto = 0.1f;


    [Header("Custom Behavier")]
    public bool seguirEnable = true;
    public bool saltoEnable = true;
    public bool direccionEnable = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGround = false;
    Seeker seeker;
    Rigidbody2D rb;
    public float distanciaLimite;
    public int saltoTotal = 1;
    public int saltos = 0;
    public WeaponController mb;


    /*[Header("Disparo")]
    public Transform enemySpawnShoot;
    public GameObject bulletEnemy;
    public float tiempoEntreDisparo = 3f;
    public float tiempoActual;
    public bool reiniciarTiempo = true;
    public int numeroBalasEnemigo = 0;
    private int totalEnemigoDisparos = 1;
    public BulletEnemy bal;*/
    public CharacterController cc;

    private void Start()
    {
        
    }
    private void Update()
    {
        SetGravity();

    }
    void SetGravity()
    {
        //this.transform.position = new Vector2(this.transform.position.x, );
        
    }







    bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

}