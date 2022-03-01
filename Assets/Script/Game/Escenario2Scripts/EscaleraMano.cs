using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraMano : MonoBehaviour
{
    Rigidbody2D rb;
    bool zonaEscalada;
    bool escalar;
    float vertical;
    public float velocidadSubir;
    JugadorMovimiento mov;
    public ArmaController arma;


    bool escondido = false;
    int escondidas = 0;
    public GameObject iconoOcultacion;
    GameObject childIcono;
    public Transform crearIcono;
    int numIconos = 0;

    [Header("Efecto Particles")]
    public Transform cuerpoPlayer;
    public GameObject ParticulaEsconderse;

    SoundManager soundManager;



    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        rb = GetComponent<Rigidbody2D>();
        mov = GetComponent<JugadorMovimiento>();
    }
    void Update()
    {
        
        if(childIcono != null)
        {
            childIcono.transform.parent = crearIcono;
        }
        Debug.Log("escondido" + escondido);
        //escondido = false;
        vertical = Input.GetAxis("Vertical");
        if (zonaEscalada && Mathf.Abs(vertical) > 0f)
        {
            escalar = true;
        }
        if (escondido)
        {
            //hacer que se quede completamente quieto en mov
            if (Input.GetKeyDown(KeyCode.O))
            {
                mov.movimentoHx = 0;
                mov.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                Instantiate(ParticulaEsconderse, cuerpoPlayer.position, Quaternion.identity);
                soundManager.SeleccionAudio(0, 1f);
                Destroy(childIcono);

                escondidas++;
                //escondido = !escondido;
                Debug.Log("Has presionado la O" + " " + escondidas);
                if (escondidas <= 1)
                {

                    arma.enabled = false;
                    mov.enabled = false;
                    Debug.Log("Estoy escondido");
                    foreach (GameObject cuerpo in mov.partesDelCuerpo)
                    {
                        cuerpo.layer = 31;
                        cuerpo.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    arma.GetComponent<SpriteRenderer>().enabled = false;

                }
                else
                {
                    childIcono = Instantiate(iconoOcultacion, crearIcono.position, Quaternion.identity);
                    arma.enabled = true;
                    mov.enabled = true;
                    Debug.Log("Ya no estoy escondido");
                    foreach (GameObject cuerpo in mov.partesDelCuerpo)
                    {
                        cuerpo.layer = 20;
                        cuerpo.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    arma.GetComponent<SpriteRenderer>().enabled = true;
                    escondidas = 0;
                }

            }




        }

        Debug.Log("Escondidas =" + " " + escondidas);
    }
    private void FixedUpdate()
    {
        if (escalar)
        {
            Debug.Log("Estoy Escalando");
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidadSubir);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EscalerasMano")
        {
            zonaEscalada = true;
            Debug.Log("Has entrado en zona de escalada");
        }
        if (collision.gameObject.tag == "Escondite")
        {
            if(numIconos < 1)
            {
                childIcono = Instantiate(iconoOcultacion, crearIcono.position, Quaternion.identity);
                
                numIconos++;
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Escondite")
        {
            escondido = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EscalerasMano")
        {
            zonaEscalada = false;
            escalar = false;
        }
        if (collision.gameObject.tag == "Escondite")
        {
            Destroy(childIcono);
            escondido = false;
            numIconos = 0;
        }
    }
}
