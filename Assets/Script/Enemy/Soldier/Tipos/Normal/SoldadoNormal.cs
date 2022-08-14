using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldadoNormal : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Objetivo")]
    public Transform player;
    public Transform enemigo_FOV;

    [Header("Range Enemy Vision")]
    public float outRange = 10;
    public float insideRange = 5;

    [Header("FOV Enemigo")]
    [SerializeField] int indexFOV;
    public float distanciaFOV;
    [SerializeField] LayerMask playerMask;

    [Range(0, 360)]
    [SerializeField] float anguloVision;
    int estadosColor;
    [Header("Perseguir Player Data")]
    public float velocidad;
    [SerializeField] private float aceleracion;
    [SerializeField] private float speed;
    [SerializeField] private float groundLinearDrag;
    public float limiteDireccion;
    public float horizontalMov; //0.6f
    public float rayDistancePlayer = 15f;

    [Header("InAir Data")]
    [SerializeField] float airLinearDrag = 2.5f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;

    [Header("Wall Data")]
    [SerializeField] Transform castRayWall;
    [SerializeField] Transform castRayWall2;
    [SerializeField] Transform ojos;
    [SerializeField] float rayWallSize;
    [SerializeField] float rayOjosSize;
    [SerializeField] float rayWallGizmos;
    [SerializeField] float rayOjosGizmos;
    Vector3 rayWallDireccion;

    bool canWall;
    bool canWall2;
    bool muro;

    [SerializeField] LayerMask detectores;

    [Header("CheckCadaver")]
    [SerializeField] Transform castRayDormido;
    [SerializeField] float sizeRayDormido;
    [SerializeField] float sizeRayDormidoGizmos;
    [SerializeField] LayerMask maskDormido;

    bool checkearDormido;

    [Header("Ground Data")]
    [SerializeField]float zonaLimita;
    [SerializeField] float groundRaycastLegth;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Transform groundRayIzquierda;
    [SerializeField] Transform groundRayDerecha;

    bool onGround;
    bool onGroundIzq;
    bool onGroundDrcha;
    bool cambiarDireccion => (rb.velocity.x > 0 && horizontalMov < 0) || (rb.velocity.x < 0 && horizontalMov > 0);

    [Header("Disparo Data")]
    [SerializeField]Transform mano;
    float cooldownDisparo;
    [SerializeField] float dispara;
    public GameObject bala;
    public Transform spawnBala;
    public float velocidadBala;

    [SerializeField] SpriteRenderer armaSprite;

    [Header("Berserker Datas")]
    [Range(0, 360)]
    [SerializeField] float toSeePlayer;
    public float leaveOutPlayer = 10;
    [HideInInspector]public bool berserker;

    [Header("Busqueda Data")]
    public float esperarModoBusquedaLimite;
    float esperarModoBusqueda;
    [SerializeField]public bool busquedaMode;
    Vector3 lockUbi;
    int block;
    int direccionFinalizada;
    bool derecha;
    float tiempoEsperaVerWall;
    int cambiarTiempoPlayer;

    [Header("Patrulla Data")]
    public List<Transform> ubicacionesDirigir = new List<Transform>();
    public int indexUbi;
    public float siguienteUbi;
    public float maxTiempoUbi;
    public float distanciaComienzaNextPatrulla = 1f;

    [Header("When see Cadaver")]
    public bool seeCadaverWarning;
    int oneTimeWarning;
    


    [Header("Señales Estados")]
    public GameObject[] señales;
    public Transform ubiSeñales;
    [SerializeField]int limitarSeñales;
    EstadoSoldado estadoAnterior;

    [Header("Llamar Soldados")]
    public float tiempoNecesarioLlamarLimite;
    float tiempoNecesarioLlamar;
    public GameObject contorno;
    public Image llamarBarra;

    [Header("Teletransportar")]
    public float distanciaStartTeleport;
    float tiempoEmpezarTeleport;
    public float tiempoEmpezarTeleportLimite;
    bool canTeleport;
    public bool wantTeleport;
    //Añadir barra UI Para señalizar teletransporte 
    //Añadir barra UI Para señalizar el evento cajas para saber que estan esperando
    //Añadir script en el circle collider que almacene un soldier life, cuando este cerca y su vida llegue a cero llamara a todos y los pondra en modo busqueda 

     enum EstadoSoldado
    {
        patrulla,
        warning,
        busqueda,
        ataque
    }

    EstadoSoldado estadoSoldado;
    SoldierLife soldierLife;
    Animator anim;

    [HideInInspector]public bool seePlayer;

    private void Awake()
    {
        //Medir Distancias
        player = GameObject.Find("Player 1").GetComponent<Transform>();
        enemigo_FOV = this.gameObject.transform.GetChild(indexFOV);

        //Movimiento
        rb = GetComponent<Rigidbody2D>();

        //Terminar ejecuciones cuando vida = 0;
        soldierLife = GetComponent<SoldierLife>();

        //Apuntar Player
        mano = this.gameObject.transform.GetChild(2);

        //Resetea el tiempo de Instanciacion de las señales
        estadoAnterior = estadoSoldado;
        limitarSeñales = 0;

        //Animaciones
        anim = GetComponent<Animator>();

        //Establecer la direccion del Rayo
        rayWallDireccion = Vector3.right;

    }

    void Update()
    {
        CancelarAnimacionCaminar();

        if (wantTeleport) Teletransportar();
        

        //Berserker
        if (!berserker)
        {
            Vision();
            armaSprite.enabled = false;
        }
        else 
        {
            estadoSoldado = EstadoSoldado.ataque;
            armaSprite.enabled = true;
        }

        //Cuando entras en alerta mode, no puedes estar en modo ataque
        if (seeCadaverWarning && !berserker) 
        {
            DetectarCadaver();
            if (oneTimeWarning < 1)
            {
                estadoSoldado = EstadoSoldado.warning;
                oneTimeWarning++;
            }
        }

    }
    private void FixedUpdate()
    {
        if(soldierLife.vida > 0)
        {
            //Limita la velocidad de Movimiento
            LimitarVelocidad();

            //Cambia la orientacion de los rayos en base a la escala
            EscalaRay();

            //3 rayos que detectan el suelo
            CheckCollisionGround();

            //1 (de momento) que detecta la pared
            CheckCollisionWall();

            //La posibilidad de encontrarte un cadaver
            CheckCadaver();


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
    void Vision()
    {

        //Angulo para saber si estas dentro del rango del FOV
        float angulo;
        angulo = Vector3.Angle(DistancenPlayer(), transform.right * transform.localScale.x);


        //Detecta Player
        //El 15 cambiar por publico
        RaycastHit2D distancia;
        distancia = Physics2D.Raycast(transform.position, DistancenPlayer().normalized, rayDistancePlayer,playerMask);

        //Cambiar estados Soldado dependiedo de la distancia;
        if (angulo < anguloVision * 0.5f)
        {
            //Si estas a menos de 5m y sacas el arma, te ven
            //if (DistancenPlayer().magnitude < 5 && ArmaController.activarDisparo && !muro) { estadoSoldado = EstadoSoldado.ataque; busquedaMode = false; berserker = true; }

            
            if (distancia && !seeCadaverWarning && !muro)
            {
                
                if (DistancenPlayer().magnitude >= outRange && !busquedaMode) estadoSoldado = EstadoSoldado.patrulla;
                else if (DistancenPlayer().magnitude < outRange && DistancenPlayer().magnitude >= insideRange) estadoSoldado = EstadoSoldado.busqueda;
                else if (DistancenPlayer().magnitude < insideRange) estadoSoldado = EstadoSoldado.ataque;
            }

            //Modo Alerta
            else if(distancia && seeCadaverWarning && !muro)
            {
                //Funciona como normalmente lo hace. Si estas a menos de 8 metros y te ve se activa
                if (DistancenPlayer().magnitude <= 8)
                {
                    Debug.Log("You are numba one da" + estadoSoldado);
                    estadoSoldado = EstadoSoldado.ataque;
                }
            }
        }
        else
        {
            if(!busquedaMode && !berserker && !seeCadaverWarning) estadoSoldado = EstadoSoldado.patrulla;
        }
        
    }
    void Apuntar()
    {
        EscalaAtaque();

        //Calcula distancia entre player y la mano
        Vector2 distanciaApuntado;
        distanciaApuntado = player.position - mano.position;
        
        //Rota la mano para apuntar al Player
        float AngleRad = Mathf.Atan2(distanciaApuntado.y,distanciaApuntado.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        mano.transform.rotation = Quaternion.Euler(0, 0, 90 + AngleDeg);

        //Disparar bala cada "x" segundos
        cooldownDisparo += Time.deltaTime;
        if (cooldownDisparo >= dispara && !player.GetComponent<EscaleraMano>().imHidden)
        {
            GameObject balaEnemigo = Instantiate(bala, spawnBala.position,Quaternion.identity);
            balaEnemigo.GetComponent<Rigidbody2D>().AddForce(spawnBala.right * velocidadBala,ForceMode2D.Impulse);
            cooldownDisparo = 0;
        }

    }
    public void SeePlayer()
    {
        float angulo;
        angulo = Vector3.Angle(DistancenPlayer(), transform.right * transform.localScale.x);

        //El 15 cambiar por publico
        RaycastHit2D distancia;
        distancia = Physics2D.Raycast(transform.position, DistancenPlayer().normalized, rayDistancePlayer, playerMask);

        //Cambiar estados Soldado dependiedo de la distancia;
        if (angulo < toSeePlayer * 0.5f)
        {
            if (distancia) {seePlayer = true; }
            //seePlayer = true;
        }
        
    }
    void Estados()
    {
        switch (estadoSoldado)
        {
            case EstadoSoldado.ataque:

                //Activa el modo Ataque
                berserker = true;
                seeCadaverWarning = false;

                //Representa el color de los Gizmos
                estadosColor = 2;
                SeePlayer();
                //Ejecuta el Movimiento y el Ataque
                //Si tu estas a menos de 10m ira a dispararte, si estas alejado vuelve a patrullar
                if (DistancenPlayer().magnitude < leaveOutPlayer && seePlayer)
                {
                   

                    //Si no estas en el borde de una zona te diriges al Player
                    if (onGroundDrcha && onGroundIzq && !canWall && !canWall2) DirigirPlayer(); 

                    //Sales del borde para dirigirte al Player 
                    else if (!onGroundIzq && DistancenPlayer().magnitude > 3) LimiteDeUnaZona();

                    //Si vuelves a entrar se vuelve a ocultar LLamarATodos
                    contorno.SetActive(false);
                    tiempoNecesarioLlamar = 0;

                    //Apuntas
                    Apuntar();
                }
                else if(DistancenPlayer().magnitude >= leaveOutPlayer && seePlayer)
                {
                    //Si sales fuera del rango deja de verme
                    seePlayer = false;
                    mano.rotation = new Quaternion(0f, 0f, 0f, 0f);
                }
                if (!seePlayer)
                {
                    if (!canTeleport) { Patrulla(); LlamarATodos(); }
                }

                //Crea la señal para tener una referencia visual de su estado
                LimitadorSeñales();
                CrearSeñales(señales[0]);

                break;
            case EstadoSoldado.busqueda:

                //Representa el color de los Gizmos
                estadosColor = 1;

                mano.rotation = new Quaternion(0f, 0f, 0f,0f);
                if (cambiarTiempoPlayer < 1) { player.GetComponent<OcultacionByWeapon>().tiempoLimiteVisible = 6f; cambiarTiempoPlayer++; }

                //Movimiento expandido

                //No te has chocado ni tampoco hay huecos en el suelo
                if (!canWall && !canWall2 && onGroundDrcha && onGroundIzq) Busqueda();

                //Giras y te diriges hacia el otro lado si hay un muro
                else if (onGroundIzq) WallCollisionBusqueda();

                //Si hay un hueco, en vez un muro, te giras y te diriges al otro lado
                else if (!onGroundIzq) LimiteDeUnaZona();

                //Busqueda desde la posicion que entra en busqueda, 10 hacia delante y 10 hacia atras. Apagando la patrulla para despues volverla a retomar
                LimitadorSeñales();
                if (busquedaMode) { armaSprite.enabled = true; CrearSeñales(señales[1]);}
                else armaSprite.enabled = false;

               

                break;
            case EstadoSoldado.patrulla:

                Patrulla();
                //Representa el color de los Gizmos
                estadosColor = 0;

                mano.rotation = new Quaternion(0f, 0f, 0f, 0f);
                block = 0;
                busquedaMode = false;

                //Resetea el valor para poder cambiar el tiempo del Player de esconderse
                if (cambiarTiempoPlayer >= 1) cambiarTiempoPlayer = 0;


                break;
            case EstadoSoldado.warning:
                Patrulla();

                estadosColor = 3;

                seeCadaverWarning = true;

                break;
            
                
                
        }
    }

    void DirigirPlayer()
    {
        //Escala en base al jugador
        //EscalaAtaque();

        //Dirigirse al jugador
        Vector3 movePosition = transform.position;
        movePosition.x = Mathf.MoveTowards(transform.position.x, player.position.x + limiteDireccion, horizontalMov * aceleracion * Time.fixedDeltaTime);
        rb.MovePosition(movePosition);
        
        //Animaciones
        if (player.position.x + limiteDireccion != transform.position.x) anim.SetBool("WalkSoldier", true);
        else if (player.position.x + limiteDireccion == transform.position.x) anim.SetBool("WalkSoldier", false);

        //Sino estas en la ubicacion walk true

        

    }
    void CheckCollisionGround()
    {
        //Rayo Central
        onGround = Physics2D.Raycast(enemigo_FOV.position * groundRaycastLegth, Vector2.down, groundRaycastLegth, groundMask);

        //Rayo Izquierda
        onGroundIzq = Physics2D.Raycast(groundRayIzquierda.position * groundRaycastLegth, Vector2.down, groundRaycastLegth, groundMask);

        //Rayo Derecha
        onGroundDrcha = Physics2D.Raycast(groundRayDerecha.position * groundRaycastLegth, Vector2.down, groundRaycastLegth, groundMask);

        if (onGround)
        {
            ApplyLinearGroundDrag();
        }

    }
    void CheckCollisionWall()
    {
        //Rayo desde la <<cadera>>
        canWall = Physics2D.Raycast(castRayWall.position, rayWallDireccion,rayWallSize, groundMask);

        //Rayo desde la cabeza
        canWall2 = Physics2D.Raycast(castRayWall2.position, rayWallDireccion,rayWallSize, groundMask);

        //Rayo desde los ojos //Aplicar solo A personajes que queden a traves de un muro
        if(!berserker) muro = Physics2D.Raycast(ojos.position, rayWallDireccion, rayOjosSize, detectores);


    }
    void CheckCadaver()
    {
        checkearDormido = Physics2D.Raycast(castRayDormido.position, rayWallDireccion, sizeRayDormido, maskDormido);

        if (checkearDormido) seeCadaverWarning = true;

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
        //Se dirige a la derecha
        if (DistancenPlayer().x > 0) { transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z); limiteDireccion = Mathf.Abs(limiteDireccion) * -1; velocidadBala = Mathf.Abs(velocidadBala) * 1; }
        //Se dirige izquierda
        else if (DistancenPlayer().x < 0) {transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z); limiteDireccion = Mathf.Abs(limiteDireccion) * 1; velocidadBala = Mathf.Abs(velocidadBala) * -1; }
        //Llega a su destino
        else transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z); 

    }
    void EscalaPatrulla()
    {
        if (indexUbi >= ubicacionesDirigir.Count) indexUbi = 0;

        Vector2 distanciaUbiDirigirse;
        distanciaUbiDirigirse = ubicacionesDirigir[indexUbi].position - transform.position;

        //Se dirige a la derecha
        if (distanciaUbiDirigirse.x > 0) transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        //Se dirige a la izquierda
        else if (distanciaUbiDirigirse.x < 0) transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        //Llega a su destino
        else transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void EscalaRay()
    {
        if (transform.localScale == new Vector3(1f, transform.localScale.y, transform.localScale.z))
        {
            //Rayo Detectar Muros
            rayWallGizmos = Mathf.Abs(rayWallGizmos) * 1;
            rayOjosGizmos = Mathf.Abs(rayOjosGizmos) * 1;
            sizeRayDormidoGizmos = Mathf.Abs(sizeRayDormidoGizmos) * 1;
            rayWallDireccion = Vector3.right;

            //Limitar Zona
            zonaLimita = 1f;

            //Modo Busqueda. Cambia en base a la direccion de a la que te diriges
            
        }
        else if (transform.localScale == new Vector3(-1f, transform.localScale.y, transform.localScale.z))
        {
            //Rayo Detectar Muros
            rayWallGizmos = Mathf.Abs(rayWallGizmos) * -1;
            rayOjosGizmos = Mathf.Abs(rayOjosGizmos) * -1;
            sizeRayDormidoGizmos = Mathf.Abs(sizeRayDormidoGizmos) * -1;
            rayWallDireccion = -Vector3.right;

            //Limitar Zona
            zonaLimita = -1;

            //Modo Busqueda. Cambia en base a la direccion de a la que te diriges
            

        }
    }
    void Busqueda()
    {
        busquedaMode = true;

        if (transform.localScale == new Vector3(1f, transform.localScale.y, transform.localScale.z)) derecha = true;
        else if (transform.localScale == new Vector3(-1f, transform.localScale.y, transform.localScale.z)) derecha = false;

        //Bloquear ubicacion actual
        if (block <= 0) { lockUbi = transform.position; block++; direccionFinalizada = 0; }

        //Registrar ubicacion del las direcciones a las que dirigirse
        Vector3 movePositionPlus = lockUbi + new Vector3(10f, 0f, 0f); //DIstance
        Vector3 movePositionMinus = lockUbi - new Vector3(10f, 0f, 0f);

        //
        if (direccionFinalizada < 2)
        {
            if (derecha)
            {
                //Te diriges a la derecha
                Vector3 movePosition = transform.position;
                movePosition.x = Mathf.MoveTowards(transform.position.x, movePositionPlus.x, horizontalMov * aceleracion * Time.fixedDeltaTime);
                rb.MovePosition(movePosition);
                int sumaDirigirse;
                sumaDirigirse = direccionFinalizada;

                //Animacion
                AnimacionCaminar(movePosition.x);

                //Al llegar a tu destino cambias la direccion
                if (transform.position.x == movePositionPlus.x)
                {
                    esperarModoBusqueda += Time.deltaTime;
                    if (esperarModoBusqueda >= esperarModoBusquedaLimite)
                    {
                        transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
                        if (direccionFinalizada < sumaDirigirse + 1) direccionFinalizada++;
                        esperarModoBusqueda = 0;
                    }
                }

            }
            else
            {
                //Te diriges a la izquierda
                Vector3 movePosition = transform.position;
                movePosition.x = Mathf.MoveTowards(transform.position.x, movePositionMinus.x, horizontalMov * aceleracion * Time.fixedDeltaTime);
                rb.MovePosition(movePosition);
                int sumaDirigirse;
                sumaDirigirse = direccionFinalizada;

                //Animacion
                AnimacionCaminar(movePosition.x);

                //Al llegar a tu destino cambias la direccion
                if (transform.position.x == movePositionMinus.x)
                {
                    esperarModoBusqueda += Time.deltaTime;
                    if (esperarModoBusqueda >= esperarModoBusquedaLimite)
                    {
                        transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
                        if (direccionFinalizada < sumaDirigirse + 1) direccionFinalizada++;
                        esperarModoBusqueda = 0;
                    }
                }
            }
        }
        else { estadoSoldado = EstadoSoldado.patrulla; busquedaMode = false; }

        
        




    }
    void WallCollisionBusqueda()
    {
        //Cuando gires seguira sin detectar uno de los dos lados
        if (canWall || canWall2 || !onGroundIzq || !onGroundDrcha) 
        {
            tiempoEsperaVerWall += Time.deltaTime;
            if(tiempoEsperaVerWall >= 2)
            {
                if (derecha) transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
                else transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);

                derecha = !derecha;
                if (direccionFinalizada < 2) direccionFinalizada++;
                else { estadoSoldado = EstadoSoldado.patrulla; busquedaMode = false; }

                tiempoEsperaVerWall = 0;
                
            }   
        }
    }
    void Patrulla()
    {
        //Funcionara si tienes una patrulla establecida
        if (ubicacionesDirigir.Count > 0)
        {
            EscalaPatrulla();

            //Si llegas a la ultima ubicacion se reinicia las localizaciones
            if (indexUbi >= ubicacionesDirigir.Count) indexUbi = 0;

            //Te diriges a una ubicacion
            Vector3 movePosition = transform.position;
            movePosition.x = Mathf.MoveTowards(transform.position.x, ubicacionesDirigir[indexUbi].position.x, horizontalMov * aceleracion * Time.fixedDeltaTime);
            movePosition.y = transform.position.y;
            rb.MovePosition(movePosition);

            //Al acercarte a la ubicacion se pasara un tiempo y cambiara la localizacion
            if (Vector2.Distance(ubicacionesDirigir[indexUbi].position, transform.position) < distanciaComienzaNextPatrulla) 
            {
                siguienteUbi += Time.deltaTime;
                if (siguienteUbi >= maxTiempoUbi)
                {
                    if (siguienteUbi >= 5 && siguienteUbi < 90) { indexUbi++; siguienteUbi = 0; }
                    else if (siguienteUbi >= 90) if(!ListaAttackSoldado.modeSoldiers.activeAllAttackSoldiers) LlamarATodos(); else { indexUbi++; siguienteUbi = 0; maxTiempoUbi = 5; }
                }
            }

            //Animacion
            //Ajustar Layer Idl + Sleep Para que solo se active una vez muerto
            if (indexUbi < ubicacionesDirigir.Count) AnimacionCaminar(ubicacionesDirigir[indexUbi].position.x);

            
        }
            


        
    }
    void LimitarVelocidad()
    {
        if (Mathf.Abs(rb.velocity.x) > speed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * velocidad, rb.velocity.y);
        }

    }
    void LimiteDeUnaZona()
    {
        Vector3 movePosition = transform.position;
        movePosition.x = Mathf.MoveTowards(transform.position.x, transform.position.x + zonaLimita, horizontalMov * aceleracion * Time.fixedDeltaTime);
        rb.MovePosition(movePosition);
    }
    public void LlamarATodos()
    {
        if (!ListaAttackSoldado.modeSoldiers.activeAllAttackSoldiers)
        {
            contorno.SetActive(true);
            llamarBarra.color = Color.green;
            llamarBarra.fillAmount = tiempoNecesarioLlamar / tiempoNecesarioLlamarLimite;
            tiempoNecesarioLlamar += Time.deltaTime;
            if (tiempoNecesarioLlamar >= tiempoNecesarioLlamarLimite)
            {
                // Aqui llamaras a la lista de statusSoldier, para colocar su mode en verdadero, tras esto el bool de listaAttack se colocara en true ya que todos estaran en verdadero, el actualStatus hara el resto 
                foreach (ListaStatusSoldierScenes statusSoldierMode in SoldierActiveInScene.instancia.soldiersStatus)
                {
                    statusSoldierMode.modeSoldier = true;
                }
                ListaAttackSoldado.modeSoldiers.AllActiveAttack();
                ListaAttackSoldado.modeSoldiers.UnactiveAllWarning();
                contorno.SetActive(false);
            }
        }
    }
    void DetectarCadaver()
    {
        if (!ListaAttackSoldado.modeSoldiers.warningAllSoldiers)
        {
            //HAcer una vez

            contorno.SetActive(true);
            llamarBarra.color = Color.yellow;
            llamarBarra.fillAmount = tiempoNecesarioLlamar / tiempoNecesarioLlamarLimite;
            tiempoNecesarioLlamar += Time.deltaTime;
            if (tiempoNecesarioLlamar >= tiempoNecesarioLlamarLimite)
            {
                foreach (ListaStatusSoldierScenes statusSoldierMode in SoldierActiveInScene.instancia.soldiersStatus)
                {
                    statusSoldierMode.warningSoldier = true;
                }
                ListaAttackSoldado.modeSoldiers.AllWarningActive();
                contorno.SetActive(false);
            }
        }
    }
    void AnimacionCaminar(float destino)
    {
        if (destino != transform.position.x) { anim.SetBool("WalkSoldier", true); }
        else if (destino == transform.position.x) { anim.SetBool("WalkSoldier", false); Debug.Log("LLegue a weska"); }
    }
    void CancelarAnimacionCaminar()
    {
        if(!onGroundDrcha || !onGroundIzq || canWall || canWall2) anim.SetBool("WalkSoldier", false);
    }

    void LimitadorSeñales()
    {
        if (estadoAnterior != estadoSoldado)
        {
            limitarSeñales = 0;
            estadoAnterior = estadoSoldado;
        }
        

    }
    void CrearSeñales(GameObject señalIcono)
    {
        if (limitarSeñales < 1)
        {
            //Debug.Log("EstadoAnterior" + " " + estadoAnterior);
            GameObject icono = Instantiate(señalIcono, ubiSeñales.position, Quaternion.identity);
            icono.transform.parent = ubiSeñales.transform;
            limitarSeñales++;
        }
    }

    void Teletransportar()
    {

        Vector2 distanciaUbiMiPos = ubicacionesDirigir[0].position - transform.position;

        if (Mathf.Abs(distanciaUbiMiPos.y) > distanciaStartTeleport)
        {
            canTeleport = true;
            contorno.SetActive(false);
            tiempoNecesarioLlamar = 0;
            tiempoEmpezarTeleport += Time.deltaTime;
            if (tiempoEmpezarTeleport >= tiempoEmpezarTeleportLimite)
            {
                //Añadir Particulas
                transform.position = ubicacionesDirigir[0].position;
                tiempoEmpezarTeleport = 0;
            }
        }
        else canTeleport = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaPlayer")) { estadoSoldado = EstadoSoldado.ataque; berserker = true; }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ascensor")) rb.drag = 0;
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
        Gizmos.color = estadosColor == 3 ? Color.magenta : estadosColor == 2 ? Color.green : estadosColor == 1 ? Color.yellow : Color.red;

        Gizmos.DrawLine(enemigo_FOV.position, (Vector2)enemigo_FOV.position + u);
        Gizmos.DrawLine(enemigo_FOV.position, (Vector2)enemigo_FOV.position + v);
        //if (player != null)Gizmos.DrawLine(enemigo_FOV.position, player.position);

        //DETECTAR SUELO
        
        //Detectar Suelo Centro
        Gizmos.color = onGround ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRaycastLegth);

        //Detectar Suelo Izquierda
        Gizmos.color = onGroundIzq ? Color.green : Color.red;
        Gizmos.DrawLine(groundRayIzquierda.position, groundRayIzquierda.position + Vector3.down * groundRaycastLegth);

        //Detectar Suelo Derecha
        Gizmos.color = onGroundDrcha ? Color.green : Color.red;
        Gizmos.DrawLine(groundRayDerecha.position, groundRayDerecha.position + Vector3.down * groundRaycastLegth);

        //DETECTAR MURO

        //Detectar muro desde la cadera
        Gizmos.color = canWall ? Color.green : Color.red;
        Gizmos.DrawLine(castRayWall.position, castRayWall.position + Vector3.right * rayWallGizmos);

        //Detectar muro desde la cabeza
        Gizmos.color = canWall2 ? Color.green : Color.red;
        Gizmos.DrawLine(castRayWall2.position, castRayWall2.position + Vector3.right * rayWallGizmos);

        //Vista de Ojo a ojo
        Gizmos.color = muro ? Color.cyan : Color.red;
        Gizmos.DrawLine(ojos.position, ojos.position + Vector3.right * rayOjosGizmos);

        //Ver Cadaver
        Gizmos.color = checkearDormido ? Color.magenta : Color.red;
        Gizmos.DrawLine(castRayDormido.position, castRayDormido.position + Vector3.right * sizeRayDormidoGizmos);
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






}
