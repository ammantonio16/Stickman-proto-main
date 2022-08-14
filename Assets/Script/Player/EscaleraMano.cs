using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraMano : MonoBehaviour
{
    [Header("Escalar Data")]
    public float velocidadSubir;
    Rigidbody2D rb;
    public bool zonaEscalada;
    bool escalar;
    float vertical;


    bool canHidden = false;
    [Header("Icono Hidden")]
    public GameObject iconoOcultacion;
    GameObject childIcono;
    public Transform crearIcono;
    int numIconos = 0;
    public int layerDesocultacion;

    JugadorMovimiento mov;
    public ArmaController arma;

    [Header("Efecto Particles")]
    public Transform cuerpoPlayer;
    public GameObject ParticulaEsconderse;

    SoundManager soundManager;
    int w;

    public bool imHidden ;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        rb = GetComponent<Rigidbody2D>();
        mov = GetComponent<JugadorMovimiento>();
        imHidden = false;
    }
    void Update()
    {

        
        if(childIcono != null && !imHidden)
        {
            childIcono.transform.parent = crearIcono;
        }

        //Subir escalera 
        Up_Down_Stairs();



        if (mov.onGround)
        {
            if (canHidden)
            {
                //hacer que se quede completamente quieto en mov
                if (Input.GetKeyDown(KeyCode.O))
                {
                    //Parar Movimiento
                    mov.horizontalMov = 0;
                    mov.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

                    //Icono
                    Instantiate(ParticulaEsconderse, cuerpoPlayer.position, Quaternion.identity);
                    soundManager.SeleccionAudio(soundManager.audiosAmbiente, 0, 1f, 0);
                    Destroy(childIcono);

                   
                    imHidden = !imHidden;
                   
                    if (imHidden)
                    {
                        
                        arma.enabled = false;
                        mov.enabled = false;
                        

                        //Cambiar Layer y tag, para ocultarte
                        mov.torso.layer = 31;
                        cuerpoPlayer.tag = "Escondite";

                    }
                    else
                    {
                        
                        childIcono = Instantiate(iconoOcultacion, crearIcono.position, Quaternion.identity);
                        arma.enabled = true;
                        mov.enabled = true;


                        ////Cambiar Layer y tag, para desocultarte
                        mov.torso.layer = layerDesocultacion;
                        cuerpoPlayer.tag = "Player";


                    }

                }
            }
        }

    }


    void Up_Down_Stairs()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (zonaEscalada && Mathf.Abs(vertical) > 0f)
        {
            escalar = true;

            if (w < 1)
            {
                soundManager.controlAudio[1].Pause();
                soundManager.controlAudio[2].Play();
                
                w++;
            }
            //Poner sonido escalera
        }
        else if (vertical == 0)
        {
            soundManager.controlAudio[2].Pause();
            w = 0;
        }
    }
    private void FixedUpdate()
    {
        if (escalar)
        {
            Debug.Log("Estoy Escalando");
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidadSubir);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EscalerasMano")
        {
            zonaEscalada = true;
        }
        if (collision.gameObject.tag == "Escondite")
        {
            if(numIconos < 1 && !imHidden)
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
            canHidden = true;
        }
        if (collision.gameObject.tag == "EscalerasMano")
        {
            zonaEscalada = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EscalerasMano")
        {
            zonaEscalada = false;
            escalar = false;
            soundManager.controlAudio[2].Stop();
            w = 0;
            //Usar el WalkSource porque necesito pararlo y con PlayOneShot no se puede parar
        }
        if (collision.gameObject.tag == "Escondite")
        {
            Destroy(childIcono);
            canHidden = false;
            numIconos = 0;
        }
    }
}
