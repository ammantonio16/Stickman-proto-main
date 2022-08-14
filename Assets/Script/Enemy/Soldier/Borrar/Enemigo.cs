using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Movimiento")]
    public float enemigoSpeed;
    //Raycast que detectan el suelo y controlan los saltos
    #region
    public Transform checkGround;//Rayo para ver visualmente si toca suelo
    public Transform checkGround2;
    public Transform checkGround3;
    public Transform checksalto;//Rayo para ver si puedes saltar o no
    public Transform checkpared;//Rayo que te permite detectar la altura a la que no puedes saltar
    #endregion
    public Transform enemigo;
    Rigidbody2D rb; //El como se va a mover el enemigo
    public float fuerzaSalto;
    public float orientacionSalto;
    bool escalaEnemigo;
    Animator anim;




    [Header("Detector y Apuntado")]
    public LayerMask objetivoVista;
    public BoxCollider2D boxCuerpo;
    public float deteccion;
    public float almacenarSombiArea;
    public GameObject mano;
    public Transform target;
    [SerializeField]
    Transform targetSombi;
    [SerializeField]
    float distanceEnemyPlayer;
    [SerializeField]
    float distanceEnemySombi;
    bool actionSombi;

    [Header("Shoot")]
    public GameObject balaEnemigo;
    public Transform spawnBala;
    public float fuerzaBala;
    public float contador;
    public float tiempoEntreDisparos;

    public float distanciaApuntor;

    [Header("Layers")]
    public LayerMask targets;

    [Header("Gizmos")]
    [Range(0f, 360f)]
    public float visionAngle = 30f;
    public float visionDistace = 10f;

    public bool detectarPlayer = false;
    public bool detectarSombi = false;

    Transform miScale;

    [Header("Propiedades Fuego")]
    public bool quemarSoldier = false;
    public int numeroQuemadurasSoldier;
    public float duracionQuemadurasSoldier;
    float dañoEntreQuemadurasSoldier;
    public GameObject fuegoSoldier;
    GameObject fuegoSoldierClon;
    //cambiar a zombieLife
    public float detectionfuego;
    int countFuego;
    public int limiteCountFuego;
    bool reseteoDetectionFuego;

    [Header("Patrullar")]
    float reactivarPatrol;
    int blockTarget = 0;
    public float frambuesa;
    public float orientacionRayoSalto = 0.55f;
    float orientacionFuego = 1;
    float orientacionFuegoEspalda = 1;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        
        
        rb = GetComponent<Rigidbody2D>();
        miScale = GetComponent<Transform>();
    }
    private void Update()
    {
        QuemadurasSoldier();
        //Calcula la distacia entre el Player y el Sombi, si existen. 
        distanceEnemyPlayer = enemigo.position.x - target.transform.position.x;
        if(targetSombi != null)
        {
            distanceEnemySombi = enemigo.position.x - targetSombi.position.x;
        }
        //Almacena al Sombi para que se coloque como objetivo
        int zombie = 1 << 26;
        Collider2D[] areaAlmacenar;
        areaAlmacenar = Physics2D.OverlapCircleAll(boxCuerpo.bounds.center, almacenarSombiArea, zombie);
        foreach (Collider2D uniTarget in areaAlmacenar)
        {
            if (blockTarget < 1)
            {
                Transform objetivo = uniTarget.GetComponentInParent<Transform>();
                targetSombi = objetivo;
                blockTarget++;
                actionSombi = true;
            }
        }
        if(targetSombi != null)
        {
            //Cuando el Sombi muere, resetea el objetivo para que se pueda volver a almacenar uno nuevo
            
        }
        //Las escalas sirven para cambiar la orientación de algunos Raycast para que no se rompan las ejecuciones
        if(miScale.localScale.x == 1)
        {
            orientacionRayoSalto = 0.55f;
            orientacionSalto = 80;
            orientacionFuego = 1f;
            orientacionFuegoEspalda = 1f;

        }
        if(miScale.localScale.x == -1)
        {
            orientacionRayoSalto = -0.55f;
            orientacionSalto = -80;
            orientacionFuego = -1f;
            orientacionFuegoEspalda = -1f;

        }

    }
    void FixedUpdate()
    {
        
        //Aquí se almacenan todos los Raycast
        #region
        int bitmask = (1 << 9);
        int fuego = (1 << 30);
        RaycastHit2D IsGrounded;
        IsGrounded = Physics2D.Raycast(checkGround.position, -Vector2.up, 0.29f, bitmask);
        RaycastHit2D IsGrounded2;
        IsGrounded2 = Physics2D.Raycast(checkGround2.position, -Vector2.up, 0.29f, bitmask);
        RaycastHit2D IsGrounded3;
        IsGrounded3 = Physics2D.Raycast(checkGround3.position, -Vector2.up, 0.29f, bitmask);
        Debug.DrawRay(checkGround.position, new Vector2(0f, -0.29f), Color.magenta);
        Collider2D seeEnemy;
        seeEnemy = Physics2D.OverlapCircle(transform.position, deteccion, objetivoVista);
        RaycastHit2D checkSalto;
        checkSalto = Physics2D.Raycast(checksalto.position, Vector2.right, orientacionRayoSalto, bitmask);
        RaycastHit2D checkPared;
        checkPared = Physics2D.Raycast(checkpared.position, Vector2.right, orientacionRayoSalto, bitmask);
        Debug.DrawRay(checkpared.position, new Vector2(orientacionRayoSalto, 0f), Color.black);
        //hacer lo mismo que orientaciones
        RaycastHit2D detectarFuego;
        detectarFuego = Physics2D.Raycast(boxCuerpo.bounds.center, Vector2.right, orientacionFuego, fuego);
        #endregion
        //Detecta si tiene fuego enfrente
        if (detectarFuego)
        {

            
        }
        if (!detectarFuego)
        {
            if(countFuego > 0)
            {
                detectionfuego = 0;
                countFuego = 0;
            }
            if(seeEnemy)
            {
                Debug.Log("Te veo");
                if (IsGrounded)
                {
                    Debug.Log("Los 3 tocan tierra");
                    if (IsGrounded3 || IsGrounded2)
                    {

                        Debug.DrawRay(checkGround.position, new Vector2(0f, -0.29f), Color.red);
                        Debug.DrawRay(checkGround2.position, new Vector2(0f, -0.29f), Color.red);
                        Debug.DrawRay(checkGround3.position, new Vector2(0f, -0.29f), Color.red);
                        if (checkSalto)
                        {
                            Debug.DrawRay(checksalto.position, new Vector2(orientacionRayoSalto, 0f), Color.green);
                            rb.AddForce(new Vector2(orientacionSalto, fuerzaSalto), ForceMode2D.Impulse);
                            if (checkPared)
                            {
                                Debug.DrawRay(checkpared.position, new Vector2(orientacionRayoSalto, 0f), Color.white);
                                Vector3 stopPosition = enemigo.position;
                                rb.MovePosition(stopPosition);
                            }
                        }
                        if (!checkSalto)
                        {
                            if (Mathf.Abs(distanceEnemyPlayer) < deteccion)
                            {
                                
                                //bool de patrulla para saber si esta activa se coloca en falso;
                                reactivarPatrol = 0;
                                if (!detectarSombi)
                                {
                                    detectarPlayer = true;
                                    if (!detectarFuego)
                                    {
                                        SeguirPlayer(target);
                                        Escala(target);
                                    }
                                    if (detectarFuego)
                                    {
                                        SeguirPlayer(this.gameObject.transform);
                                        Escala(target);
                                    }
                                }
                            }
                            if (actionSombi)
                            {
                                if (Mathf.Abs(distanceEnemySombi) < deteccion)
                                {

                                    
                                    reactivarPatrol = 0;
                                    if (!detectarPlayer)
                                    {
                                        detectarSombi = true;
                                        if (!detectarFuego)
                                        {
                                            SeguirPlayer(targetSombi);
                                            Escala(targetSombi);
                                        }
                                        if (detectarFuego)
                                        {
                                            SeguirPlayer(this.gameObject.transform);
                                            Escala(targetSombi);
                                        }
                                    }
                                }
                            }
                            if (Mathf.Abs(distanceEnemyPlayer) > deteccion)
                            {
                                reactivarPatrol += Time.deltaTime;
                                detectarPlayer = false;
                                if (reactivarPatrol >= 5 && !detectarFuego)
                                {
                                    contador = 0;
                                    reactivarPatrol = 5;
                                   
                                    mano.transform.rotation = Quaternion.Euler(0, 0, 0);
                                }
                            }
                            if (Mathf.Abs(distanceEnemySombi) > deteccion)
                            {
                                StartCoroutine("ResetTarget");
                                detectarSombi = false;
                                reactivarPatrol += Time.deltaTime;
                                if (reactivarPatrol >= 5 && !detectarFuego)
                                {
                                    contador = 0;
                                    reactivarPatrol = 5;
                                    
                                    mano.transform.rotation = Quaternion.Euler(0, 0, 0);
                                }
                            }

                        }
                    }

                }

                if (!IsGrounded2 || !IsGrounded3)
                {
                    Debug.Log("Ya no toco tierra");
                    if (!detectarPlayer && detectarSombi && Mathf.Abs(distanceEnemySombi) < deteccion)
                    {
                        SeguirPlayer(targetSombi);
                        Apuntar(targetSombi);
                    }
                    if (!detectarSombi && detectarPlayer && Mathf.Abs(distanceEnemyPlayer) < deteccion)
                    {
                        SeguirPlayer(target);
                        Apuntar(target);

                    }
                }
            }
            else
            {
                //comprobar si al pasar el tiempo se ejecuta, porque no tengo ni puta idea de si el float se quedara a 0 o incrementara con el tiempo
                //sino pones una variable arriba y fuera
                detectarPlayer = false;
                float reactPatrol;
                reactPatrol = 0;
                reactPatrol += Time.deltaTime;
                if(reactPatrol >= 2)
                {
                    
                }
            }
            
        }
        
    }
    void SeguirPlayer(Transform chosen)
    {
        int fuego = (1 << 30);
        int bitmask = (1 << 9);
        RaycastHit2D IsGrounded2;
        IsGrounded2 = Physics2D.Raycast(checkGround2.position, -Vector2.up, 0.29f, bitmask);
        RaycastHit2D IsGrounded3;
        IsGrounded3 = Physics2D.Raycast(checkGround3.position, -Vector2.up, 0.29f, bitmask);
        RaycastHit2D detectarFuegoEspalda;
        detectarFuegoEspalda = Physics2D.Raycast(boxCuerpo.bounds.center, Vector2.left, orientacionFuegoEspalda, fuego);
        Debug.DrawRay(boxCuerpo.bounds.center, new Vector2(orientacionFuegoEspalda, 0f) * Vector2.left, Color.white);
        //Dirección a la que va el enemigo: el "Sombi"
        if (detectarSombi)
        {
            if (Mathf.Abs(distanceEnemySombi) < distanciaApuntor)
            {
                Debug.Log("Me has apuntado?");
                //Apuntar(chosen);
            }
            if (Mathf.Abs(distanceEnemySombi) > 3)
            {
                Debug.Log("Distancia > 3");
                Vector3 movePosition = enemigo.position;
                movePosition.x = Mathf.MoveTowards(transform.position.x, chosen.transform.position.x, enemigoSpeed * Time.deltaTime);
                rb.MovePosition(movePosition);
            }
            if (distanceEnemySombi < 3.1 && distanceEnemySombi > -3.1)
            {
                Debug.Log("Distancia entre 3.1");
                Vector3 movePosition = enemigo.position;
                rb.MovePosition(movePosition);
            }
            if (!detectarFuegoEspalda && IsGrounded2 && IsGrounded3)
            {
                if (Mathf.Abs(distanceEnemySombi) < 3)
                {
                    Debug.Log("Distancia en < 3");
                    Vector3 movePosition = enemigo.position;
                    movePosition.x = Mathf.MoveTowards(transform.position.x, chosen.transform.position.x, -enemigoSpeed * Time.deltaTime);
                    rb.MovePosition(movePosition);
                }
            }
            
        }
        //Dirección a la que va el enemigo: el "player"
        if (detectarPlayer)
        {
            if (Mathf.Abs(distanceEnemyPlayer) < distanciaApuntor)
            {
                //Apuntar(chosen);
            }
            if (Mathf.Abs(distanceEnemyPlayer) > 3)
            {
                Debug.Log("DistancePlayer > 3");
                Vector3 movePosition = enemigo.position;
                movePosition.x = Mathf.MoveTowards(transform.position.x, chosen.transform.position.x, enemigoSpeed * Time.deltaTime);
                rb.MovePosition(movePosition);
            }
            if (distanceEnemyPlayer < 3.1 && distanceEnemyPlayer > -3.1)
            {
                Debug.Log("DistanciaPlayer entre 3.1");
                Vector3 movePosition = enemigo.position;
                rb.MovePosition(movePosition);
            }
            if (!detectarFuegoEspalda && IsGrounded2 && IsGrounded3)
            {
                if (Mathf.Abs(distanceEnemyPlayer) < 3)
                {
                    Debug.Log("DistancePlayer < 3");
                    Vector3 movePosition = enemigo.position;
                    movePosition.x = Mathf.MoveTowards(transform.position.x, chosen.transform.position.x, -enemigoSpeed * Time.deltaTime);
                    rb.MovePosition(movePosition);
                }
            }
            
        }
    }
    void Apuntar(Transform objective)
    {
        if(objective != null)
        {
            float AngleRad = Mathf.Atan2(objective.transform.position.y - mano.transform.position.y, objective.transform.position.x - mano.transform.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            mano.transform.rotation = Quaternion.Euler(0, 0, 90 + AngleDeg);
            Debug.DrawRay(spawnBala.position, (objective.transform.position - mano.transform.position), Color.blue);
        }
        contador += Time.deltaTime;
        if (contador >= tiempoEntreDisparos)
        {
            GameObject balaEnemigoclon = Instantiate(balaEnemigo, spawnBala.position, balaEnemigo.transform.rotation);
            if (escalaEnemigo)
            {
                balaEnemigoclon.GetComponent<Rigidbody2D>().AddForce(spawnBala.right * 100 * fuerzaBala);
                contador = 0;
            }
            if (!escalaEnemigo)
            {
                balaEnemigoclon.GetComponent<Rigidbody2D>().AddForce(-spawnBala.right * 100 * fuerzaBala);
                contador = 0;
            }
        }
    }
    void Escala(Transform priority)
    {
        if (priority.transform.position.x > enemigo.transform.position.x)
        {
            enemigo.transform.localScale = new Vector2(1, 1);
            escalaEnemigo = true;
        }
        else
        {
            enemigo.transform.localScale = new Vector2(-1, 1);
            escalaEnemigo = false;
        }

    }
    void QuemadurasSoldier()
    {
        if (quemarSoldier)
        {
            duracionQuemadurasSoldier += Time.deltaTime;
            if (numeroQuemadurasSoldier < 1)
            {
                Debug.Log("Se ha instanciado el fuego bien Sombi");
                fuegoSoldierClon = Instantiate(fuegoSoldier, miScale.transform);
                numeroQuemadurasSoldier++;
            }
            if (fuegoSoldierClon != null && Time.time > dañoEntreQuemadurasSoldier + 2f)
            {
                //vida.DañoRecibidoZombie(10);
                
                dañoEntreQuemadurasSoldier = Time.time;
                Debug.Log("El fuego existe Sombi");
            }
            if (duracionQuemadurasSoldier >= 10)
            {
                quemarSoldier = false;
                Destroy(fuegoSoldierClon);
                numeroQuemadurasSoldier = 0;
                duracionQuemadurasSoldier = 0;
                dañoEntreQuemadurasSoldier = Time.time;
            }
        }
    }
    IEnumerator ResetTarget()
    {
        yield return new WaitForSeconds(0.1f);
        blockTarget = 0;
        targetSombi = null;
        actionSombi = false;
    }
    private void OnDrawGizmos()
    {
        if (visionAngle <= 0) return;

        float halfVisionAngle = visionAngle / 2;

        Vector2 u, v;

        u = PointForAngle(halfVisionAngle, visionDistace);
        v = PointForAngle(-halfVisionAngle, visionDistace);

        Gizmos.color = detectarPlayer ? Color.green : Color.red;
        if (detectarSombi)
        {
            Gizmos.color = Color.yellow;
        }

        Gizmos.DrawLine(boxCuerpo.bounds.center, (Vector2)transform.position + u);
        Gizmos.DrawLine(boxCuerpo.bounds.center, (Vector2)transform.position + v);
    }
    Vector3 PointForAngle(float angle, float distance)
    {
        return boxCuerpo.transform.TransformDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))) * distance;
    }
}


