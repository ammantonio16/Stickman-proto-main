using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;

public class JugadorMovimiento : MonoBehaviour
{
    public float fuerza;
    Rigidbody2D rb;
    Transform myTransform;
    public static bool direccionBala;

    [Header("Atributos Movimiento")]
    public float velocidad;
    bool escalaX;

    [Header("Atributos Salto")]
    public float fuerzaSalto;
    public Collider2D piernas;
    public bool saltoEnabled;
    public bool aumentoSalto;

    [Header("Animacion")]
    public Animator anim;
    public ArmaController orientacion;

    [Header("Camuflaje")]
    public GameObject[] partesDelCuerpo;
    bool camuflaje;
    bool modoCadaver;
    bool golpeRayEspalda;
    public float tiempoCamuflaje;

    public Transform puntoEspalda;
    public float espaldaRayDistance;
    int levantarse;

    [Header("Fuego Propiedades")]
    public static bool quemar;
    public float duracionQuemaduras = 10f;
    public float dañoEntreQuemaduras = 0;
    public GameObject fuegoPrueba;
    GameObject fuegoClon;
    public int numeroQuemaduras = 0;
    Life2Enemy vidaPlayer;


    void Start()
    {
        vidaPlayer = GetComponent<Life2Enemy>();
        levantarse = 0;
        direccionBala = true;
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        modoCadaver = false;
    }

    void Update()
    {
        Quemaduras();
        int bitmask = (1 << 9);
        RaycastHit2D IsGround;
        IsGround = Physics2D.Raycast(piernas.bounds.center, -Vector2.up, 0.45f, bitmask);
        RaycastHit2D RayoEspaldaPlayer;
        RayoEspaldaPlayer = Physics2D.Raycast(puntoEspalda.position, -puntoEspalda.right, espaldaRayDistance, bitmask);
        if (camuflaje)
        {
            tiempoCamuflaje += Time.deltaTime;
            if(tiempoCamuflaje >= 60)
            {
                if(modoCadaver)
                {
                    levantarse = 0;
                    foreach (GameObject cuerpo in partesDelCuerpo)
                    {
                        cuerpo.GetComponent<Collider2D>().enabled = true;
                    }
                    if (levantarse <= 0)
                    {
                        myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y + 0.1f, myTransform.position.z);
                        levantarse++;
                    }
                    if (RayoEspaldaPlayer)
                    {
                        Debug.Log("Activado RayoEspaldaPlayer");
                        golpeRayEspalda = false;
                        rb.bodyType = RigidbodyType2D.Dynamic;
                    }
                    anim.SetBool("Camuflaje", false);
                }
                foreach (GameObject cuerpo in partesDelCuerpo)
                {
                    cuerpo.layer = 20;
                    camuflaje = false;
                    
                }
                modoCadaver = false;
            }
            if(tiempoCamuflaje < 60)
            {
                if (IsGround)
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        modoCadaver = !modoCadaver;
                        Debug.Log("Has presionado la letra C");
                        if (modoCadaver)
                        {
                            levantarse = 0;
                            anim.SetBool("Camuflaje", true);
                            foreach (GameObject cuerpo in partesDelCuerpo)
                            {
                                cuerpo.layer = 29;
                            }
                        }
                        else
                        {  
                            foreach (GameObject cuerpo in partesDelCuerpo)
                            {
                                cuerpo.GetComponent<Collider2D>().enabled = true;
                            }
                            foreach (GameObject cuerpo in partesDelCuerpo)
                            {
                                cuerpo.layer = 26;
                            }
                            if (levantarse <= 0)
                            {
                                myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y + 0.1f, myTransform.position.z);
                                levantarse++;
                            }
                            anim.SetBool("Camuflaje", false);
                        }
                    }
                }
                if (RayoEspaldaPlayer)
                {
                    golpeRayEspalda = true;
                    if (modoCadaver)
                    {
                        rb.bodyType = RigidbodyType2D.Static;
                        foreach (GameObject cuerpo in partesDelCuerpo)
                        {
                            cuerpo.GetComponent<Collider2D>().enabled = false;
                        }
                    }
                }
                if (!RayoEspaldaPlayer)
                {
                    golpeRayEspalda = false;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                
            }
        }
    }
    private void FixedUpdate()
    {
        if (!modoCadaver)
        {
            CambiarEscala();
            float x = Input.GetAxis("Horizontal");
            int bitmask = (1 << 9);
            RaycastHit2D IsGround;
            IsGround = Physics2D.Raycast(piernas.bounds.center, -Vector2.up, 0.45f, bitmask);
            Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.5f), Color.red);
            int cinta = (1 << 22);
            RaycastHit2D IsCinta;
            IsCinta = Physics2D.Raycast(piernas.bounds.center, -Vector2.up, 0.45f, cinta);
            Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.5f), Color.red);
            if (IsGround)
            {
                Movimiento();
                saltoEnabled = true;
                anim.SetBool("Jump", false);
            }
            if (IsGround && Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("Jump", true);
                saltoEnabled = false;
                Movimiento();
                rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            }
            if (!IsGround)
            {
                aumentoSalto = false;
                fuerzaSalto = 5;
                rb.velocity = new Vector2(x * velocidad, rb.velocity.y);
                if (x > 0)
                {
                    if (!ArmaController.activarDisparo)
                    {
                        myTransform.localScale = new Vector3(1, 1, 1);
                    }
                    anim.SetBool("Walk", false);
                }
                else if (x < 0)
                {
                    if (CambiarEscalaConMira.calculo.x > 0)
                    {
                        velocidad = 2;
                    }
                    if (!ArmaController.activarDisparo)
                    {
                        myTransform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else
                {
                    anim.SetBool("Walk", false);
                }
                if (ArmaController.activarDisparo)
                {
                    orientacion.brazo.up = orientacion.brazo.position - orientacion.mira.position;
                }

            }
        }


    }
    void Movimiento()
    {
        float x = Input.GetAxis("Horizontal");
        //Debug.Log("El valor del mocimiento del personaje es " + " " + x);
        rb.velocity = new Vector2(x * velocidad, 0f);
        aumentoSalto = true;
        if (aumentoSalto)
        {
            fuerzaSalto = 7;
        }
        if (x > 0)
        {
            anim.SetBool("Walk", true);
            velocidad = 5;
            if (ArmaController.activarDisparo)
            {
                if (escalaX)
                {
                    velocidad = 5;
                }
                else
                {
                    velocidad = 2;
                }
            }
            
            if(!ArmaController.activarDisparo)
            {
                myTransform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (x < 0)
        {
            if(escalaX && CambiarEscalaConMira.calculo.x > 0)
            {
                velocidad = 2;
            }
            if (!escalaX && CambiarEscalaConMira.calculo.x < 0)
            {
                velocidad = 5;
            }
            if (!ArmaController.activarDisparo)
            {
                myTransform.localScale = new Vector3(-1, 1, 1);
            }
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
            aumentoSalto = false;
            fuerzaSalto = 5;
        }
    }
    void CambiarEscala()
    {
        if(myTransform.localScale.x == 1)
        {
            direccionBala = true;
            escalaX = true;
            
        }
        if (myTransform.localScale.x == -1)
        {
            escalaX = false;
            direccionBala = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = golpeRayEspalda ? Color.white : Color.red;
        Gizmos.DrawRay(puntoEspalda.position, -puntoEspalda.right * espaldaRayDistance);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cadaver")
        {
            Debug.Log("Te puedes camuflar");
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Has presionado la P");
                foreach(GameObject cuerpo in partesDelCuerpo)
                {
                    cuerpo.layer = 26;
                    camuflaje = true;
                    tiempoCamuflaje = 0;
                }
            }
        }
        if(collision.gameObject.tag == "Fuego")
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAH!");
        }
    }

    void Quemaduras()
    {
        if (quemar)
        {
            anim.SetBool("Quemar", true);
            duracionQuemaduras += Time.deltaTime;
            if(numeroQuemaduras < 1)
            {
                Debug.Log("Se ha instanciado el fuego bien");
                fuegoClon = Instantiate(fuegoPrueba, myTransform.transform);
                numeroQuemaduras++;
            }
            if(fuegoClon != null && Time.time > dañoEntreQuemaduras + 2f)
            {
                vidaPlayer.VidaBaja(10);
                dañoEntreQuemaduras = Time.time;
                Debug.Log("El fuego existe");
            }
            if(duracionQuemaduras >= 10)
            {
                quemar = false;
                Destroy(fuegoClon);
                numeroQuemaduras = 0;
                duracionQuemaduras = 0;
                dañoEntreQuemaduras = Time.time;
            }
        }
        else
        {
            anim.SetBool("Quemar", false);
        }
    }
}
