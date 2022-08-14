using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado_DetectarEchar : MonoBehaviour
{
    Rigidbody2D rb;
    //Player Objetivo
    [Header("Objetivo")]
    public Transform player;
    public Transform enemigo_FOV;

    [Header("FOV Enemigo")]
    [SerializeField] int indexFOV;
    public float distanciaFOV;
    [SerializeField] LayerMask playerMask;

    [Range(0,360)]
    [SerializeField] float anguloVision;
    int estadosColor;
    [Header("Perseguir Player Data")]
    public float velocidad;
    [SerializeField] private float aceleracion;
    [SerializeField] private float speed;
    [SerializeField] private float groundLinearDrag;
    public float limiteDireccion;
    public float horizontalMov; //0.6f

    [Header("InAir Data")]
    //[SerializeField] float jumpForce = 12f;
    [SerializeField] float airLinearDrag = 2.5f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;

    bool cambiarDireccion => (rb.velocity.x > 0 && horizontalMov < 0) || (rb.velocity.x < 0 && horizontalMov > 0);

    [Header("Berserker")]
    public bool berserker;
    public GameObject detectar_Echar;

    [Header("Ground Data")]
    [SerializeField] float groundRaycastLegth;
    bool onGround;
    [SerializeField] LayerMask groundMask;

    [Header("Disparo Data")]
    [SerializeField]Transform mano;
    float cooldownDisparo;
    [SerializeField] float dispara;
    public GameObject bala;
    public Transform spawnBala;
    public float velocidadBala;

    [Header("Señales Estados")]
    public GameObject[] señales;
    public Transform ubiSeñales;
    int limitarSeñales;
    EstadoSoldado estadoAnterior;


    enum EstadoSoldado
    {
        patrulla,
        ataque
    }

    EstadoSoldado estadoSoldado;
    SoldierLife soldierLife;
    int llamarAnalisis;
    private void Awake()
    {
        //Medir Distancias
        player = GameObject.Find("Player 1").GetComponent<Transform>();
        enemigo_FOV = this.gameObject.transform.GetChild(indexFOV);    

        Debug.Log("La childCount" +" "+ this.gameObject.transform.GetChild(4));

        //Movimiento
        rb = GetComponent<Rigidbody2D>();

        //Terminar ejecuciones cuando vida = 0;
        soldierLife = GetComponent<SoldierLife>();

        //Apuntar Player
        mano = this.gameObject.transform.GetChild(2);

        //Analizar la lista de soldados
        llamarAnalisis = 0;

        //Resetea el tiempo de Instanciacion de las señales
        estadoAnterior = estadoSoldado;
        limitarSeñales = 0;

    }

    
    void Update()
    {
        if (!berserker) DistanciaPlayer();
        if (soldierLife.vida <= 0) detectar_Echar.SetActive(false);

    }
    private void FixedUpdate()
    {
        if(soldierLife.vida > 0)
        {
            CheckCollisionGround();
            if (onGround)
            {
                Estados();
                ApplyLinearGroundDrag();
            }
            else
            {
                ApplyLinearAirDrag();
                FallMultiplier();
            }
        }
        
    }
    void Apuntar()
    {
        //Calcula distancia entre player y la mano
        Vector2 distanciaApuntado;
        distanciaApuntado = player.position - mano.position;
        
        //Rota la mano para apuntar al Player
        float AngleRad = Mathf.Atan2(distanciaApuntado.y,distanciaApuntado.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        mano.transform.rotation = Quaternion.Euler(0, 0, 90 + AngleDeg);

        //Disparar bala cada "x" segundos
        cooldownDisparo += Time.deltaTime;
        if(cooldownDisparo >= dispara)
        {
            GameObject balaEnemigo = Instantiate(bala, spawnBala.position,Quaternion.identity);
            balaEnemigo.GetComponent<Rigidbody2D>().AddForce(spawnBala.right * velocidadBala,ForceMode2D.Impulse);
            cooldownDisparo = 0;
        }

    }
    void Estados()
    {
        switch (estadoSoldado)
        {
            case EstadoSoldado.ataque:

                //Representa el color de los Gizmos
                estadosColor = 2;

                //Ejecuta el Movimiento y el Ataque
                DirigirPlayer();
                Apuntar();

                //Activa el modo Ataque
                berserker = true;

                //Anula el evento y el que te detecte con un mayor rango
                detectar_Echar.SetActive(false);

                //Crea la señal para tener una referencia visual de su estado
                LimitadorSeñales();
                if (limitarSeñales < 1){ GameObject señalAtaque = Instantiate(señales[0], ubiSeñales.position, Quaternion.identity);
                    señalAtaque.transform.parent = ubiSeñales.transform;
                    limitarSeñales++; }
                break;
            case EstadoSoldado.patrulla:

                //Representa el color de los Gizmos
                estadosColor = 0;

                mano.rotation = new Quaternion(0f, 0f, 0f, 0f);
                berserker = false;

                break;
        }
    }
    void DistanciaPlayer()
    {

        //Direccion del Raycast normalizada
        //Debug.Log(DistancenPlayer());

        //Angulo para saber si estas dentro del rango del FOV
        float angulo;
        angulo = Vector3.Angle(DistancenPlayer(), transform.right * transform.localScale.x);


        //Detecta Player
        RaycastHit2D distancia;
        distancia = Physics2D.Raycast(transform.position, DistancenPlayer().normalized, 15, playerMask);

        //Cambiar estados Soldado dependiedo de la distancia;
        if (angulo < anguloVision * 0.5f)
        {
            if (distancia)
            {
                if (DistancenPlayer().magnitude >= 10) estadoSoldado = EstadoSoldado.patrulla;
                else if (DistancenPlayer().magnitude < 5) estadoSoldado = EstadoSoldado.ataque;
            }
        }
        else
        {
            if (!berserker) estadoSoldado = EstadoSoldado.patrulla;
        }

    }

    void DirigirPlayer()
    {
        //Escala en base al jugador
        EscalaAtaque();

        //Dirigirse al jugador
        Vector3 movePosition = transform.position;
        movePosition.x = Mathf.MoveTowards(transform.position.x, player.position.x + limiteDireccion, horizontalMov * aceleracion * Time.fixedDeltaTime);
        rb.MovePosition(movePosition);

        //Limita la velocidad
        if (Mathf.Abs(rb.velocity.x) > speed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * velocidad, rb.velocity.y);
        }

    }
    void CheckCollisionGround()
    {
        onGround = Physics2D.Raycast(enemigo_FOV.position * groundRaycastLegth, Vector2.down, groundRaycastLegth, groundMask);
        if (onGround)
        {
            ApplyLinearGroundDrag();
        }

    }
    void FallMultiplier()
    {
        if (rb.velocity.y < 0) //Si estas cayendo
        {
            rb.gravityScale = fallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
        //Por si quiero hacerle saltar
        /*
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump")) //Si acabas de saltar
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }*/

    }
    void ApplyLinearGroundDrag()
    {
        if (Mathf.Abs(horizontalMov) < 1 || cambiarDireccion)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    void ApplyLinearAirDrag()
    {
        rb.drag = airLinearDrag;
    }
    void EscalaAtaque()
    {
        //Se dirige al derecha
        if (DistancenPlayer().x > 0) { transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z); limiteDireccion = Mathf.Abs(limiteDireccion) * -1; velocidadBala = Mathf.Abs(velocidadBala) * 1; }
        //Se dirige izquierda
        else if (DistancenPlayer().x < 0) {transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z); limiteDireccion = Mathf.Abs(limiteDireccion) * 1; velocidadBala = Mathf.Abs(velocidadBala) * -1; }
        //Llega a su destino
        else transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z); 

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaPlayer")) { estadoSoldado = EstadoSoldado.ataque; berserker = true; }
    }

    //Representacion de rayos
    private void OnDrawGizmos()
    {
        //DETECTAR DISTANCIA PLAYER
        if (anguloVision <= 0) return;

        float halfVisionAngle = anguloVision * 0.5f;

        Vector2 u, v;

        u = PointForAngle(halfVisionAngle, distanciaFOV);
        v = PointForAngle(-halfVisionAngle, distanciaFOV);

        //Dependiendo de la distancia los colores cambian
        Gizmos.color = estadosColor == 2 ? Color.green : estadosColor == 1 ? Color.yellow : Color.red; 

        Gizmos.DrawLine(enemigo_FOV.position, (Vector2)enemigo_FOV.position + u);
        Gizmos.DrawLine(enemigo_FOV.position, (Vector2)enemigo_FOV.position + v);
        //Gizmos.DrawLine(enemigo_FOV.position, player.position);

        //DETECTAR SUELO

        Gizmos.color = onGround ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRaycastLegth);

    }

    

    //Ángulo de visión
    Vector3 PointForAngle(float angle, float distance)
    {
        return enemigo_FOV.TransformDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))) * distance;
    }


    //Distancia entre el enemigo y el Player
    Vector2 DistancenPlayer()
    {
        Vector2 distanciaPlayer;
        return distanciaPlayer = player.position - enemigo_FOV.position;
    }
    void LimitadorSeñales()
    {
        if (estadoAnterior != estadoSoldado)
        {
            limitarSeñales = 0;
            estadoAnterior = estadoSoldado;
            Debug.Log("Reset Señales");
        }
        
    }
}
