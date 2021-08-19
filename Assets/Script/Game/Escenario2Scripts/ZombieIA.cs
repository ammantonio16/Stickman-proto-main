using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIA : MonoBehaviour
{
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
    public Transform[] enemies;
    int countEnemies;
    Vector2 enemiesVector;
    Vector2 playerVector;
    float velocidad = 2;
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
    void Start()
    {
        patrulla = GetComponent<Patrulla>();
        rayoEspalda = true;
        rbSombi = GetComponent<Rigidbody2D>();
        transformSombi = GetComponent<Transform>();
        vidaSombi = GetComponent<ZombieLife>();
        foreach (Transform enemyPos in enemies)
        {
            countEnemies++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        QuemadurasSombi();

        playerToAttack = false;
        BalaDirection();
        distancePlayerSombi = transformSombi.position.x - player.position.x;
        detectarEnemy = false;
        detectarPlayer = false;
        playerVector = player.position - cabezaVista.position;
        angleHeadWithVectorPlayer = Vector3.Angle(playerVector.normalized, cabezaVista.right);

        for (int i = 0; i < countEnemies; i++)
        {
             enemiesVector = enemies[i].position - cabezaVista.position;
        }
        angleHeadWithVectorEnemy = Vector3.Angle(enemiesVector.normalized, cabezaVista.right);

        RaycastHit2D seeEnemy;
        seeEnemy = Physics2D.CircleCast(cabezaVista.position, radioVista, Vector2.right, distanciaVista, objetivoVista);
        if (seeEnemy)
        {
            if (transformSombi.localScale.x == 1)
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
                            if (distancePlayerSombi >= radioAnimationAttack || distancePlayerSombi <= -radioAnimationAttack)
                            {
                                detectarPlayer = true;
                            }
                            else
                            {
                                detectarPlayer = false;
                            }
                            Vector3 targetPlayer = player.position - cabezaVista.position;
                            cabezaVista.right = targetPlayer;
                            angleHeadPlayer = Vector3.Angle(targetPlayer, transform.right);
                            if (angleHeadPlayer > 90)
                            {
                                transformSombi.localScale = new Vector3(-1f, 1f, 1f);
                            }
                        }
                    }
                }
                /*if (Vector3.Angle(playerVector.normalized, cabezaVista.right) > visionAngle / 2 && vidaSombi.vidaZombie > 0)
                {
                    reactivarPatrulla += Time.deltaTime;
                    Debug.Log("El tiempo para reactivar el script de Patrulla es de" + " " + reactivarPatrulla);
                    if (reactivarPatrulla >= 5)
                    {
                        patrulla.enabled = true;
                    }
                }*/
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
                            Vector3 targetEnemy = enemies[0].position - cabezaVista.position;
                            cabezaVista.right = targetEnemy;
                            angleHeadEnemy = Vector3.Angle(targetEnemy, transform.right);
                            if (angleHeadEnemy > 90)
                            {
                                transformSombi.localScale = new Vector3(-1f, 1f, 1f);
                                
                            }
                        }
                    }
                }
            }
            if(transformSombi.localScale.x == -1)
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
                            if (distancePlayerSombi >= 0.5 || distancePlayerSombi <= -0.5)
                            {
                                detectarPlayer = true;
                            }
                            else
                            {
                                detectarPlayer = false;
                            }
                            Vector3 targetPlayer = player.position - cabezaVista.position;
                            cabezaVista.right = -targetPlayer;
                            angleHeadPlayer = Vector3.Angle(targetPlayer, transform.right);
                            if (angleHeadPlayer < 90)
                            {
                                transformSombi.localScale = new Vector3(1f, 1f, 1f);
                            }
                        }
                    }
                }
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
                            Vector3 targetEnemy = enemies[0].position - cabezaVista.position;
                            cabezaVista.right = -targetEnemy;
                            angleHeadEnemy = Vector3.Angle(targetEnemy, transform.right);

                        }
                    }
                }
            }
            
        }
        else
        {
            //Colocar desactivar patrulla aqui,playerVector.magnitude < visionDistace 
            if (playerVector.magnitude > visionDistace && vidaSombi.vidaZombie > 0)
            {
                reactivarPatrulla += Time.deltaTime;
                if(reactivarPatrulla >= 5)
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
                movePosition.x = Mathf.MoveTowards(transform.position.x, enemiesVector.x, velocidad * Time.deltaTime);
                rbSombi.MovePosition(movePosition);
            }
            if (detectarPlayer)
            {
                Vector3 movePosition = transformSombi.position;
                movePosition.x = Mathf.MoveTowards(transform.position.x, player.position.x, velocidad * Time.deltaTime);
                rbSombi.MovePosition(movePosition);
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
        
        Collider2D animationAttack;
        animationAttack = Physics2D.OverlapCircle(transformSombi.position, radioAnimationAttack);
        RaycastHit2D IsAttack;
        IsAttack = Physics2D.Raycast(attackCuerpo.position, attackCuerpo.right, radioAnimationAttack, playerAttack);
        if (IsAttack)
        {
            playerToAttack = true;
            detectarPlayer = false;
            Vector3 rbSombiStop = transformSombi.position;
            rbSombi.MovePosition(rbSombiStop);
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            sonidoAttack.Play();
            Debug.Log("Golpea");
        }
        /*if (animationAttack.gameObject.tag == "Player" || animationAttack.gameObject.tag == "Enemy")
        {
            detectarPlayer = false;
            Vector3 rbSombiStop = transformSombi.position;
            rbSombi.MovePosition(rbSombiStop);
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            sonidoAttack.Play();

        }*/
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
