using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Assertions.Must;

public class IA : MonoBehaviour
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



    [Header("Disparo")]
    public Transform enemySpawnShoot;
    public GameObject bulletEnemy;
    public float tiempoEntreDisparo = 3f;
    public float tiempoActual;
    public bool reiniciarTiempo = true;
    public int numeroBalasEnemigo = 0;
    private int totalEnemigoDisparos = 1;
    public BulletEnemy bal;

    [Header("StepClimp")]
    [SerializeField] GameObject piernas;
    [SerializeField] GameObject pies;
    [SerializeField] float StepHeight = 0.3f;
    [SerializeField] float StepSmooth = 0.1f;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);


    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Steps();
        if (!Turn.turnos)
        {
            mb.numeroDisparos = 0;
            if (!Turn.detectorMapa)
            {
                StartCoroutine(Detectar(1));
                Turn.detectorMapa = true;
            }

            StartCoroutine("PruebaLimite");
            if (Vector2.Distance(transform.position, target.position) < distanciaLimite)
            {
                path = null;
                return;
            }
        }

    }
    public IEnumerator PruebaLimite()
    {
        yield return new WaitForSeconds(3f);
        PathFollow();
    }
    public IEnumerator Disparar(float tiempo)
    {
        yield return new WaitForSeconds(3f);
        TiempoEspera();
    }
    public IEnumerator Detectar(float tiempo)
    {
        yield return new WaitForSeconds(1f);
        AstarPath.active.Scan();
    }
    public void TiempoEspera()
    {
        if (numeroBalasEnemigo < totalEnemigoDisparos)
        {
            Instantiate(bulletEnemy, enemySpawnShoot.position, enemySpawnShoot.rotation);
            bal.rangobala = new Vector2(Random.Range(bal.rangoBala2.position.x, bal.rangoBala1.position.x), bal.rangoBala3.position.y);
            numeroBalasEnemigo++;
            Debug.Log(bal.rangobala);
        }
    }
    private void UpdatePath()
    {
        if (seguirEnable && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);

        }
    }
    void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + modificadorSalto);
        isGround = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);



        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * velocidad * Time.deltaTime;


        if (saltoEnable && isGround)
        {
            if (direction.y > nodeTamañoSaltoRequerido)
            {
                rb.AddForce(Vector2.up * modificadorSalto * velocidad);

            }
        }
        //if (!isGround)
        //{ 
        //force.y = 0;
        //}
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        if (direccionEnable)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            saltos = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target.position, distanciaLimite);
    }
    void Steps()
    {

    }
}
