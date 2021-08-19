using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Movimiento")]
    public float enemigoSpeed;
    public Transform checkGround;//Rayo para ver visualmente si toca suelo
    public Transform checksalto;//Rayo para ver si puedes saltar o no
    public Transform checkpared;//Rayo que te permite detectar la altura
    public Transform enemigo;
    Rigidbody2D rb; //El como se va a mover el enemigo
    public Collider2D colliderCuerpoPlayer;//El punto origen para saber la distancia que guarda con el enemigo
    public Collider2D colliderCuerpoSombi;
    public float fuerzaSalto;
    public float pruebaVelocidad;
    public bool escalaEnemigo;
    public Animator anim;

    


    [Header("Detector y Apuntado")]
    public BoxCollider2D boxCuerpo;
    public float deteccion;
    public float radioDisparo;
    public float radioPlayer;
    public GameObject mano;
    public GameObject target;
    public GameObject targetSombi;

    [Header("Shoot")]
    public GameObject balaEnemigo;
    public Transform spawnBala;
    public Rigidbody2D cuerpoBala;
    public float fuerzaBala;
    public float contador;
    public float tiempoEntreDisparos;
    public int municion;

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
    Life2Enemy vidaEnemy;
    public float detectionfuego;
    int countFuego;
    public int limiteCountFuego;
    bool resetDetectionFuego;

    [Header("Patrullar")]
    Patrulla patrulla;
    float reactivarPatrol;
    void Start()
    {
        patrulla = GetComponent<Patrulla>();
        vidaEnemy = GetComponent<Life2Enemy>();
        rb = GetComponent<Rigidbody2D>();
        miScale = GetComponent<Transform>();
    }
    private void Update()
    {
        //detectarPlayer = false;
        QuemadurasSoldier();
    }
    void FixedUpdate()
    {
        detectarPlayer = false;
        detectarSombi = false;
        int bitmask = (1 << 9);
        int fuego = (1 << 30);
        RaycastHit2D IsGrounded;
        IsGrounded = Physics2D.Raycast(checkGround.position, -Vector2.up, 0.29f, bitmask);
        Debug.DrawRay(checkGround.position, new Vector2(0f, -0.29f), Color.magenta);
        RaycastHit2D checkSalto;
        checkSalto = Physics2D.Raycast(checksalto.position, Vector2.right, 0.55f, bitmask);
        RaycastHit2D checkSaltoIzq;
        checkSaltoIzq = Physics2D.Raycast(checksalto.position, -Vector2.right, 0.55f, bitmask);
        RaycastHit2D checkPared;
        checkPared = Physics2D.Raycast(checkpared.position, Vector2.right, 0.55f, bitmask);
        Debug.DrawRay(checkpared.position, new Vector2(0.55f, 0f), Color.black);
        RaycastHit2D checkParedIzq;
        checkParedIzq = Physics2D.Raycast(checkpared.position, -Vector2.right, 0.55f, bitmask);
        RaycastHit2D detectarFuego;
        detectarFuego = Physics2D.Raycast(boxCuerpo.bounds.center, Vector2.right, 1f, fuego);
        if (detectarFuego)
        {
            detectionfuego += Time.deltaTime;
            if (detectionfuego < 4)
            {
                patrulla.enabled = false;
                Debug.Log("Ramba");
            }
            if (countFuego < limiteCountFuego)
            {
                patrulla.puntosUbi++;
                Debug.Log("El numero actual de puntos ubicacion es" + " " + patrulla.puntosUbi);
                countFuego++;
            }
            if (detectionfuego >= 4)
            {
                patrulla.enabled = true;
                resetDetectionFuego = true;
            }
        }
        if (IsGrounded)
        {
            Debug.DrawRay(checkGround.position, new Vector2(0f, -0.29f), Color.red);
            if (escalaEnemigo)
            {
                if (checkSalto)
                {
                    Debug.DrawRay(checksalto.position, new Vector2(0.55f, 0f), Color.green);
                    rb.AddForce(new Vector2(1, fuerzaSalto), ForceMode2D.Impulse);
                    if (checkPared)
                    {
                        Debug.DrawRay(checkpared.position, new Vector2(0.55f, 0f), Color.white);
                        Vector3 stopPosition = enemigo.position;
                        rb.MovePosition(stopPosition);
                    }
                }
                if (!checkSalto)
                {
                    Debug.DrawRay(checksalto.position, new Vector2(0.55f, 0f), Color.blue);
                    //Area de Vision del enemigo
                    Collider2D areadeteccion;
                    areadeteccion = Physics2D.OverlapCircle(boxCuerpo.bounds.center, deteccion, targets);
                    //Cuendo detecta a "player"
                    if (areadeteccion)
                    {
                        patrulla.enabled = false;
                        reactivarPatrol = 0;
                        if (areadeteccion.gameObject.tag == "Sombis")
                        {
                            Debug.Log("Sombi te he visto");
                            detectarSombi = true;
                            if (!detectarFuego)
                            {
                                SeguirPlayer(targetSombi, colliderCuerpoSombi);
                                Escala(targetSombi);
                            }
                            if (detectarFuego)
                            {
                                SeguirPlayer(this.gameObject, colliderCuerpoSombi);
                                Escala(targetSombi);
                            }
                        }
                        if (areadeteccion.gameObject.tag == "Player")
                        {
                            Debug.Log("Player te he visto");
                            detectarPlayer = true;
                            if (!detectarFuego)
                            {
                                SeguirPlayer(target, colliderCuerpoPlayer);
                                Escala(target);
                            }
                            if (detectarFuego)
                            {
                                SeguirPlayer(this.gameObject, colliderCuerpoPlayer);
                                Escala(target);
                            }
                        }
                        //Escala();

                    }
                    if (!areadeteccion)
                    {

                        reactivarPatrol += Time.deltaTime;
                        if (reactivarPatrol >= 5 && !detectarFuego)
                        {
                            reactivarPatrol = 5;
                            patrulla.enabled = true;
                        }
                        mano.transform.rotation = Quaternion.Euler(0, 0, 0);
                        municion = 0;
                    }
                }
            }
            if (!escalaEnemigo)
            {
                if (checkSaltoIzq)
                {
                    Debug.DrawRay(checksalto.position, new Vector2(-0.55f, 0f), Color.green);
                    rb.AddForce(new Vector2(-1, fuerzaSalto), ForceMode2D.Impulse);
                    if (checkParedIzq)
                    {
                        Vector3 stopPosition = enemigo.position;
                        rb.MovePosition(stopPosition);
                    }
                }
                if (!checkSaltoIzq)
                {

                    Debug.DrawRay(checksalto.position, new Vector2(0.55f, 0f), Color.blue);
                    //Area de Vision del enemigo
                    Collider2D areadeteccion;
                    areadeteccion = Physics2D.OverlapCircle(boxCuerpo.bounds.center, deteccion, targets);
                    //Cuendo detecta a "player"
                    if (areadeteccion)
                    {
                        patrulla.enabled = false;
                        reactivarPatrol = 0;
                        if(areadeteccion.gameObject.tag == "Sombis")
                        {
                            Debug.Log("Sombi te he visto");
                            detectarSombi = true;
                            SeguirPlayer(targetSombi, colliderCuerpoSombi);
                            Escala(targetSombi);
                        }
                        if (areadeteccion.gameObject.tag == "Player")
                        {
                            Debug.Log("Player te he visto");
                            detectarPlayer = true;
                            SeguirPlayer(target, colliderCuerpoPlayer);
                            Escala(target);
                        }
                    }
                    if (!areadeteccion)
                    {
                        reactivarPatrol+= Time.deltaTime;
                        if (reactivarPatrol >= 5)
                        {
                            reactivarPatrol = 5;
                            patrulla.enabled = true;
                        }
                        contador = 0;
                        mano.transform.rotation = Quaternion.Euler(0, 0, 0);
                        municion = 0;
                    }
                }
            }
        }
    }
    void SeguirPlayer(GameObject chosen, Collider2D chosenCollider2D)
    {
        //Dirección a la que va el enemigo: el "player"
        Vector3 movePosition = enemigo.position;
        movePosition.x = Mathf.MoveTowards(transform.position.x, chosen.transform.position.x, enemigoSpeed * Time.deltaTime);
        rb.MovePosition(movePosition);
        if (escalaEnemigo)
        {
            rb.velocity = new Vector2(pruebaVelocidad * Time.deltaTime, 0f);
        }
        if (!escalaEnemigo)
        {
            rb.velocity = new Vector2(-pruebaVelocidad * Time.deltaTime, 0f);
        }
        int arealimite = (1 << 14);
        Collider2D areaPlayer;
        areaPlayer = Physics2D.OverlapCircle(chosenCollider2D.bounds.center, radioPlayer, arealimite);
        Collider2D areaDisparo;
        areaDisparo = Physics2D.OverlapCircle(chosenCollider2D.bounds.center, radioDisparo, arealimite);
        if (areaDisparo)
        {
            Apuntar(chosen);
        }
        if (areaPlayer)
        {
            movePosition = enemigo.position;
            rb.MovePosition(movePosition);
        }
    }
    void Apuntar(GameObject objective)
    {
        float AngleRad = Mathf.Atan2(objective.transform.position.y - mano.transform.position.y, objective.transform.position.x - mano.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        mano.transform.rotation = Quaternion.Euler(0, 0, 90 + AngleDeg);
        Debug.DrawRay(spawnBala.position, (objective.transform.position - mano.transform.position), Color.blue);
        contador += Time.deltaTime;
        if (contador >= tiempoEntreDisparos)
        {
            GameObject balaEnemigoclon = Instantiate(balaEnemigo, spawnBala.position, balaEnemigo.transform.rotation);
            //fuerzaBala = Random.Range(10, 40);

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
    void Escala(GameObject priority)
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

        Gizmos.DrawLine(boxCuerpo.bounds.center, (Vector2)boxCuerpo.bounds.center + u);
        Gizmos.DrawLine(boxCuerpo.bounds.center, (Vector2)boxCuerpo.bounds.center + v);
    }

    Vector3 PointForAngle(float angle, float distance)
    {
        return boxCuerpo.transform.TransformDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))) * distance;
    }

    private void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.tag == ("BalaPlayer"))
        {
            anim.SetTrigger("EnemyHurt");
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
                vidaEnemy.VidaBaja(10);
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
}


