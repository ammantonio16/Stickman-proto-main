using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor;

public class EnemyIA : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Física")]
    public float velocidad = 200f;
    public float velocidadSalto = 800f;
    public float nextWaypointDistance = 3f;
    public float nodeTamañoSaltoRequerido = 0.8f;
    public float modificadorSalto = 0.3f;
    public float controladorSalto = 0.1f;
    Vector2 direction;
    

    [Header("Custom Behavier")]
    public bool seguirEnable = true;
    public bool saltoEnable = false;
    public bool direccionEnable = true;
    public bool raycastSuelo;
    public Transform positionPlayer;

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
    public int numeroBalasEnemigo = 0;
    private int totalEnemigoDisparos = 1;
    public BulletEnemy bal;

    [Header("StepClimp")]
    [SerializeField] GameObject piernas;
    [SerializeField] GameObject pies;
    [SerializeField] GameObject cuello;
    [SerializeField] GameObject cabeza;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 0.01f;
    [SerializeField] LayerMask layerMask;

     public bool continuarRecorrido = true;



    private void Awake()
    {
        //piernas.transform.position = new Vector3(piernas.transform.position.x, stepHeight, piernas.transform.position.z);
    }
    void Start()
    {
        
        stepHeight = 0.3f;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);

    }

    // Update is called once per frame
    public void FixedUpdate()
    {


        if (!Turn.turnos)
        {
            Steps();
            mb.numeroDisparos = 0;
            if (!Turn.detectorMapa)
            {
                StartCoroutine(Detectar(1));
                Turn.detectorMapa = true;
            }
            if (continuarRecorrido)
            {
                StartCoroutine("PruebaLimite");
            }
            if (Vector2.Distance(transform.position, target.position) < distanciaLimite)
            {
                path = null;
                StartCoroutine("TiempoEspera");
            }
        }
        
    }
    /*void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);

        SlopeCheckVertical(checkPos);
    }
    void SlopeCheckHorizontal(Vector2 checkPos)
    {

    }
    void SlopeCheckVertical(Vector2 checkPos)
    {
        int bitmask = (1 << 11);
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, bitmask);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal);

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                isonSlop = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }*/
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
            rb.position = rb.position;
            return;
            
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        //Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + modificadorSalto);
        //isGround = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);


        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * velocidad * Time.deltaTime;
        rb.AddForce(force);

        /*if (saltoEnable && isGround)
        {
            if (direction.y > nodeTamañoSaltoRequerido)
            {
                rb.AddForce(Vector2.up * modificadorSalto * velocidad);
                saltoEnable = false;
            }
        }*/
        /*if (isGround && !isonSlop)
        {
            force = new Vector3(force.x, 0.0f, 0.0f);
        }else if (isGround && isonSlop)
        {
            force = new Vector3(velocidad * -direction * slopeNormalPerp, force.y * slopeNormalPerp);
        }else if (!isGround)
        {
            rb.velocity = force;
        }*/

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (positionPlayer.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
            Turn.direccionbalaEnemy = false;
            raycastSuelo = false;
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
            Turn.direccionbalaEnemy = true;
            raycastSuelo = true;
        }
    }
    bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    void Salto()
    {
        if (saltoEnable)
        {

            if (direction.y > nodeTamañoSaltoRequerido)
            {
                rb.AddForce(Vector2.up * modificadorSalto * velocidad);
                saltoEnable = false;
            }
        }
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
            //saltoEnable = true;
        }
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target.position, distanciaLimite);
        
    }*/
        void Steps()
    {
        int bitmask = (1 << 11);
        if (raycastSuelo)
        {
            RaycastHit2D coll = Physics2D.Raycast(cuello.transform.position, Vector2.right, 0.2f, bitmask);
            RaycastHit2D pie = Physics2D.Raycast(pies.transform.position, Vector2.right, 0.1f, bitmask);
            Debug.DrawRay(pies.transform.position, Vector2.right, Color.blue);
            if (pie)
            {
                RaycastHit2D pierna = Physics2D.Raycast(piernas.transform.position, Vector2.right, 0.2f, bitmask);
                Debug.DrawRay(piernas.transform.position, Vector2.right, Color.blue);
                if (!pierna)
                {
                    rb.position -= new Vector2(0f, stepSmooth);
                }
                if (coll)
                {
                    RaycastHit2D cap = Physics2D.Raycast(cabeza.transform.position, Vector2.right, 0.3f, bitmask);
                    rb.AddForce(new Vector2(0f, velocidadSalto), ForceMode2D.Impulse);
                    if (cap)
                    {
                        continuarRecorrido = false;
                        StopCoroutine("PruebaLimite");
                    }
                }
            }
        }
        if (!raycastSuelo)
        {
            RaycastHit2D coll = Physics2D.Raycast(cuello.transform.position, -Vector2.right, 0.2f, bitmask);
            RaycastHit2D pie = Physics2D.Raycast(pies.transform.position, -Vector2.right, 0.1f, bitmask);
            Debug.DrawRay(pies.transform.position, Vector2.right, Color.blue);
            if (pie)
            {
                RaycastHit2D pierna = Physics2D.Raycast(piernas.transform.position, -Vector2.right, 0.2f, bitmask);
                Debug.DrawRay(piernas.transform.position, -Vector2.right, Color.blue);
                if (!pierna)
                {
                    rb.position -= new Vector2(0f, stepSmooth);
                }
                if (coll)
                {
                    RaycastHit2D cap = Physics2D.Raycast(cabeza.transform.position, -Vector2.right, 0.3f, bitmask);
                    rb.AddForce(new Vector2(0f, velocidadSalto),ForceMode2D.Impulse);
                    if (cap.collider)
                    {
                        continuarRecorrido = false;
                        StopCoroutine("PruebaLimite");
                    }
                }
            }
        }      
    }
}