using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIA : MonoBehaviour
{
    [Header("ID")]
    public int idZombie;
    public Transform checkGround;
    public Transform cabezaVista;
    public float radioVista;
    public float radioAnimationAttack;
    public float distanciaVista;
    public LayerMask objetivoVista;
    Rigidbody2D rbSombi;
    Transform transformSombi;
    ZombieLife vidaSombi;
    public static bool tocarGround;
    public bool atraccion;

    [Range(0f, 360f)]
    public float visionAngle = 30f;
    public float visionDistace = 10f;

    public bool detectarPlayer = false;
    public bool detectarEnemy = false;
    public bool alarmaCoche = false;

    public Transform player;
    public Transform enemies;
    public Vector2 enemiesVector;
    public Vector2 playerVector;
    public float velocidad = 2;
    float angleHeadPlayer;
    float angleHeadEnemy;
    float angleHeadWithVectorPlayer;
    float angleHeadWithVectorEnemy;

    public float distancePlayerSombi;

    public AudioSource sonidoAttack;

    [SerializeField]
    BoxCollider2D cuerpo;

    public bool rayoEspalda;

    public Transform attackCuerpo;
    public LayerMask playerAttack;
    bool playerToAttack;
    [Header("Propiedades Fuego")]
    public bool quemarSombi = false;
    public int numeroQuemadurasSombi;
    public float duracionQuemadurasSombi;
    float dañoEntreQuemadurasSombi;
    public GameObject fuegoSombi;
    GameObject fuegoSombiClon;
    [Header("Patrullar")]
    Patrulla patrulla;
    float reactivarPatrulla;

    public bool parar;
    public Transform almacenarEnemy;
    public float radioAlmacen;
    public int blockTarget;
    public float pomelo;

    //Probar enum y switch
    void Start()
    {
        parar = true;
        patrulla = GetComponent<Patrulla>();
        rayoEspalda = true;
        rbSombi = GetComponent<Rigidbody2D>();
        transformSombi = GetComponent<Transform>();
        vidaSombi = GetComponent<ZombieLife>();
        
    }

    // Update is called once per frame
    void Update()
    {
        QuemadurasSombi();
        BalaDirection();

        parar = true;
        playerToAttack = false;
        detectarEnemy = false;
        detectarPlayer = false;

        distancePlayerSombi = transformSombi.position.x - player.position.x;
        playerVector = player.position - cabezaVista.position;
        angleHeadWithVectorPlayer = Vector3.Angle(playerVector.normalized, cabezaVista.right);

        int enemy = 1 << 14;
        Collider2D seeEnemy;
        seeEnemy = Physics2D.OverlapCircle(transform.position, radioVista, objetivoVista);

        //Almacena al enemigo y cuando él muere lo quita del almacenaje
        #region
        Collider2D[] areaAlmacen;
        areaAlmacen = Physics2D.OverlapCircleAll(almacenarEnemy.position, radioAlmacen, enemy);
        //Calculo para girar la cabeza cuando  ve al enemigo
        if (enemies != null)
        {
            enemiesVector = enemies.transform.position - cabezaVista.position;
            angleHeadWithVectorEnemy = Vector3.Angle(enemiesVector.normalized, cabezaVista.right);
        }
        //Almacena a un único enemigo en una variable. Solo a uno para que no se vuelva loco y cambie constantemente de Target
        foreach (Collider2D uniTarget in areaAlmacen)
        {
            if (blockTarget < 1)
            {
                Transform patata = uniTarget.GetComponentInParent<Transform>();
                enemies = patata;
                blockTarget++;
            }
        }
        //Cuando muere el enemigo, resetea todos los valores para poder almacenar de nuevo otro.
        if(enemies != null)
        {
            if(enemies.GetComponentInParent<ZombieLife>().vidaZombie <= 0)
            {
                gameObject.GetComponent<Animator>().ResetTrigger("Attack");
                gameObject.GetComponentInChildren<ZombieDamege>().enemy = null;
                pomelo += Time.deltaTime;
                if(pomelo >= 0.2)
                {
                    blockTarget = 0;
                    enemies = null;
                    detectarEnemy = false;
                }
            }
        }
        #endregion

        //Dejas de almacenar al Player para que deje de atacarte en el suelo
        #region
        if (player != null)
        {
            if(player.GetComponent<LifePlayer>().actualLife <= 0)
            {
                player = null;
                detectarPlayer = false;
            }
        }
        #endregion

        //El Player o el Enemy han entrado en la visión del Sombi
        
        if (seeEnemy)
        {
            if (transformSombi.localScale.x == 1)
            {
                if (!detectarEnemy)
                {
                    if (Vector3.Angle(playerVector.normalized, cabezaVista.right) < visionAngle / 2 && vidaSombi.vidaZombie > 0)
                    {
                        if (playerVector.magnitude < visionDistace)
                        {
                            if (!alarmaCoche)
                            {
                                reactivarPatrulla = 0;
                                patrulla.enabled = false;
                                AnimationAttack();

                                //Se frena a cierta distancia para no estar constantemente pegado al Player
                                #region
                                if (Mathf.Abs(distancePlayerSombi) <= 1)
                                {
                                    parar = false;
                                }
                                if (Mathf.Abs(distancePlayerSombi) >= 0.5)
                                {
                                    detectarPlayer = true;
                                }
                                #endregion

                                //Girar la cabeza cuando ve al Player
                                #region
                                Vector3 targetPlayer = player.position - cabezaVista.position;
                                cabezaVista.right = targetPlayer;
                                angleHeadPlayer = Vector3.Angle(targetPlayer, transform.right);
                                //Cuando la cabeza mire arriba, el cuerpo también girará
                                if (angleHeadPlayer > 90)
                                {
                                    transformSombi.localScale = new Vector3(-1f, 1f, 1f);
                                }
                                if (cabezaVista.rotation.z > -10)
                                {
                                    Debug.Log("Malasaña");
                                }
                                #endregion
                            }
                        }
                    }
                }
                if (!detectarPlayer && enemies != null)
                {
                    if (Vector3.Angle(enemiesVector.normalized, cabezaVista.right) < visionAngle / 2 && vidaSombi.vidaZombie > 0)
                    {
                        if (enemiesVector.magnitude < visionDistace)
                        {
                            if (!alarmaCoche)
                            {
                                reactivarPatrulla = 0;
                                patrulla.enabled = false;
                                AnimationAttack();
                                detectarEnemy = true;
                                if(Mathf.Abs(enemiesVector.x) <= 1)
                                {
                                    parar = false;
                                }
                                //Girar cabeza hacia la dirección enemigo
                                #region
                                Vector3 targetEnemy = enemies.transform.position - cabezaVista.position;
                                cabezaVista.right = targetEnemy;
                                angleHeadEnemy = Vector3.Angle(targetEnemy, transform.right);
                                //Girar el cuerpo cuando la cabeza este mirando arriba
                                if (angleHeadEnemy > 90)
                                {
                                    transformSombi.localScale = new Vector3(-1f, 1f, 1f);
                                }
                                #endregion
                            }
                        }
                    }
                }

            }
            if(transformSombi.localScale.x == -1)
            {
                if (!detectarEnemy)
                {
                    if (Vector3.Angle(playerVector.normalized, -cabezaVista.right) < visionAngle / 2 && vidaSombi.vidaZombie > 0)
                    {
                        if (playerVector.magnitude < visionDistace)
                        {
                            if (!alarmaCoche)
                            {
                                reactivarPatrulla = 0;
                                patrulla.enabled = false;
                                AnimationAttack();
                                if (Mathf.Abs(distancePlayerSombi) <= 1)
                                {
                                    parar = false;
                                }
                                if (Mathf.Abs(distancePlayerSombi) >= 0.5)
                                {
                                    detectarPlayer = true;
                                }
                                
                                Vector3 targetPlayer = player.position - cabezaVista.position;
                                cabezaVista.right = -targetPlayer;
                                angleHeadPlayer = Vector3.Angle(targetPlayer, transform.right);
                                if (angleHeadPlayer < 90)
                                {
                                    transformSombi.localScale = new Vector3(1f, 1f, 1f);
                                }
                                if (cabezaVista.rotation.z < 0)
                                {
                                    Debug.Log("Malasaña");
                                }
                            }
                        }
                    }
                }
                if (!detectarPlayer && enemies != null)
                {
                    if (Vector3.Angle(enemiesVector.normalized, -cabezaVista.right) < visionAngle / 2 && vidaSombi.vidaZombie > 0)
                    {
                        if (enemiesVector.magnitude < visionDistace)
                        {
                            if (!alarmaCoche)
                            {
                                reactivarPatrulla = 0;
                                patrulla.enabled = false;
                                AnimationAttack();
                                detectarEnemy = true;
                                if (Mathf.Abs(enemiesVector.x) <= 1)
                                {
                                    parar = false;
                                }
                                if (enemies != null)
                                {
                                    Vector3 targetEnemy = enemies.transform.position - cabezaVista.position;
                                    cabezaVista.right = -targetEnemy;
                                    angleHeadEnemy = Vector3.Angle(targetEnemy, transform.right);
                                }

                            }
                        }
                    }
                }
            } 
        }

        //El Player o El Enemy han salido del rango de visión del Sombi
        else
        {
            //Si el Sombi esta vivo, vuelve a patrullar. Player
            if (playerVector.magnitude > visionDistace && vidaSombi.vidaZombie > 0)
            {
                reactivarPatrulla += Time.deltaTime;
                if(reactivarPatrulla >= 5)
                {
                    reactivarPatrulla = 5;
                    patrulla.enabled = true;
                }
            }
            //Resetea los valores del almacenaje y vuelve a patrullar. Enemy
            if (enemiesVector.magnitude > distanciaVista && blockTarget == 1)
            {
                enemies = null;
                blockTarget = 0;
                reactivarPatrulla += Time.deltaTime;
                if (reactivarPatrulla >= 5)
                {
                    reactivarPatrulla = 5;
                    patrulla.enabled = true;
                }
            }
            
        }
    }
    private void FixedUpdate()
    {
        
        //Debug.Log("La distancia entre el objetivo y sombi es de" + " " + distanceX);
        int bitmask = (1 << 9);
        RaycastHit2D IsGrounded;
        IsGrounded = Physics2D.Raycast(checkGround.position, -checkGround.up, 0.29f, bitmask);
        RaycastHit2D RayEspalda;
        RayEspalda = Physics2D.Raycast(checkGround.position, -checkGround.right, 0.17f, bitmask);
        
        if (RayEspalda)
        {
            rayoEspalda = false;
        }
        if (IsGrounded)
        {   
            tocarGround = true;
        }
        else
        {
            Debug.Log("Ya no esta tocando el suelo el Zombie");
            tocarGround = false;
        }
        if (tocarGround)
        {
            if (detectarEnemy)
            {

                Vector3 movePosition = transformSombi.position;
                if(transformSombi.localScale.x == 1)
                {
                    movePosition.x = Mathf.MoveTowards(transform.position.x, enemiesVector.x, -velocidad * Time.deltaTime);
                }
                if(transformSombi.localScale.x == -1)
                {
                    movePosition.x = Mathf.MoveTowards(transform.position.x, enemiesVector.x, velocidad * Time.deltaTime);
                }
                if (parar)
                {
                    rbSombi.MovePosition(movePosition);
                }
                
                
            }
            if (detectarPlayer)
            {
                if (parar)
                {
                    Vector3 movePosition = transformSombi.position;
                    movePosition.x = Mathf.MoveTowards(transform.position.x, player.position.x, velocidad * Time.deltaTime);
                    rbSombi.MovePosition(movePosition);
                }
                
                
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        if (visionAngle <= 0) return;

        float halfVisionAngle = visionAngle / 2;

        Vector2 u, v;

        u = PointForAngle(halfVisionAngle, visionDistace);
        v = PointForAngle(-halfVisionAngle, visionDistace);

        //Detectar Player
        Gizmos.color = detectarPlayer ? Color.green : Color.red;
        //Detectar Enemy
        if (detectarEnemy)
        {
            Gizmos.color = Color.cyan;
        }

        Gizmos.DrawLine(cabezaVista.position, (Vector2)cabezaVista.position + u);
        Gizmos.DrawLine(cabezaVista.position, (Vector2)cabezaVista.position + v);


        //Rayo Suelo
        Gizmos.color = tocarGround ? Color.green : Color.red;
        Gizmos.DrawRay(checkGround.position, -checkGround.up * 0.29f);
        //Rayo Espalda
        Gizmos.color = rayoEspalda ? Color.yellow : Color.red;
        Gizmos.DrawRay(checkGround.position, -checkGround.right * 0.17f);
        //Rayo Attack
        Gizmos.color = playerToAttack ? Color.white : Color.red;
        Gizmos.DrawRay(attackCuerpo.position, attackCuerpo.right * radioAnimationAttack);
    }

    Vector3 PointForAngle(float angle, float distance)
    {
        return cabezaVista.TransformDirection (new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))) * distance;
    }

    void AnimationAttack()
    {
        RaycastHit2D IsAttack;
        IsAttack = Physics2D.Raycast(attackCuerpo.position, attackCuerpo.right, radioAnimationAttack + 1, playerAttack);
        if (IsAttack && player.GetComponent<LifePlayer>().actualLife > 0 || IsAttack && enemies.GetComponent<ZombieLife>().vidaZombie > 0)
        {
            playerToAttack = true;
            //detectarPlayer = false;
            Vector3 rbSombiStop = transformSombi.position;
            rbSombi.MovePosition(rbSombiStop);
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            sonidoAttack.Play();
            Debug.Log("Golpea");
        }
        if(Mathf.Abs(distancePlayerSombi) > 1 || Mathf.Abs(enemiesVector.x) > 1)
        {
            parar = true;
        }
        
    }

    void BalaDirection()
    {
        if (transformSombi.localScale.x == -1)
        {
            attackCuerpo.localEulerAngles = new Vector3(0f, 180f, 0f);
            
        }
        if (transformSombi.localScale.x == 1)
        {
            attackCuerpo.localEulerAngles = new Vector3(0f, 0f, 0f);
            
        }
    }
    void QuemadurasSombi()
    {
        if (quemarSombi)
        {
            duracionQuemadurasSombi += Time.deltaTime;
            if (numeroQuemadurasSombi < 1)
            {
                Debug.Log("Se ha instanciado el fuego bien Sombi");
                fuegoSombiClon = Instantiate(fuegoSombi, transformSombi.transform);
                numeroQuemadurasSombi++;
            }
            if (fuegoSombiClon != null && Time.time > dañoEntreQuemadurasSombi + 2f)
            {
                vidaSombi.DañoRecibidoZombie(10);
                dañoEntreQuemadurasSombi = Time.time;
                Debug.Log("El fuego existe Sombi");
            }
            if (duracionQuemadurasSombi >= 10)
            {
                quemarSombi = false;
                Destroy(fuegoSombiClon);
                numeroQuemadurasSombi = 0;
                duracionQuemadurasSombi = 0;
                dañoEntreQuemadurasSombi = Time.time;
            }
        }
    }
}
