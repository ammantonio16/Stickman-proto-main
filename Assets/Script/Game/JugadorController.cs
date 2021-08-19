using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Juador controller fue creado y habilitado, para con los controles de android, poder controlar al player 

public class JugadorController : MonoBehaviour
{
    Rigidbody2D rb;
    public static bool izquierda;
    public static bool derecha;
    public bool checkSalto;
    public bool arriba;
    public float velocidad;
    public float direccionCinta;
    public float potenciaSalto;
    float movimientoHorizintal;
    public Collider2D piernas;
    Transform escala;
    public bool colisioncajasfuturas;
    public bool cinto;
    public Animator anim;
    bool Walk;


    public WeaponController balaCambiar;
    public PowerUpBala balaEmpuje;
    public PowerUpBala balaNegra;
    public bool comprobar;
    public GameObject balaOriginal;
    public GameObject antorcha;
    public GameObject luz1;
    //public ContadordeTiempo ct;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        escala = GetComponent<Transform>();
        izquierda = false;
        derecha = false;
        arriba = false;
        Turn.turnos = true;
    }
    void Update()
    {
        if (Turn.turnos)
        {
            //ct.TiempoRestante();
            Movimiento();
            Power(balaOriginal);
            if (Input.GetKey(KeyCode.D)) 
            {
                derecha = true;
                anim.SetBool("Walk", true);
            }
            if (Input.GetKey(KeyCode.A)) 
            {
                izquierda = true;
                anim.SetBool("Walk", true);
            }
            if (Input.GetKeyUp(KeyCode.D)) 
            {
                derecha = false;
                anim.SetBool("Walk", false);
            }
            if (Input.GetKeyUp(KeyCode.A)) 
            {
                izquierda = false;
                anim.SetBool("Walk", false);
            }
            
        }
    }

    public void IzquierdaBoton()
    {
        izquierda = true;
        anim.SetBool("Walk", true);
    }
    public void DerechaBoton()
    {
        derecha = true;
        anim.SetBool("Walk", true);
    }
    public void IzquierdaBotonUp()
    {
        izquierda = false;
        anim.SetBool("Walk", false);
    }
    public void DerechaBotonUp()
    {
        derecha = false;
        anim.SetBool("Walk", false);
    }
    private void FixedUpdate()
    {
        int bitmask = (1 << 9);
        RaycastHit2D IsGround;
        IsGround = Physics2D.Raycast(piernas.bounds.center, -Vector2.up, 0.5f, bitmask);
        Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.5f), Color.red);
        int cinta = (1 << 22);
        RaycastHit2D IsCinta;
        IsCinta = Physics2D.Raycast(piernas.bounds.center, -Vector2.up, 0.45f, cinta);
        Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.5f), Color.red);
        if (IsGround)
        {
            Salto();
            checkSalto = true;
            rb.velocity = new Vector2(movimientoHorizintal, rb.velocity.y);
            Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.5f), Color.green);
            //pruebaanimacionsalto
            anim.SetBool("Tierra", true);
            anim.SetBool("Jump", false);
        }
        if (!IsGround)
        {
            rb.velocity = new Vector2(movimientoHorizintal, rb.velocity.y);
            checkSalto = false;
            anim.SetBool("Tierra", false);
            anim.SetBool("Jump", true);
        }
        if (IsCinta)
        {
            Salto();
            checkSalto = true;
            rb.velocity = new Vector2(movimientoHorizintal, rb.velocity.y);
            Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.45f), Color.green);
        }
        if (cinto)
        {
            rb.AddForce(Vector2.right * direccionCinta * Time.deltaTime);
        }


    }
    void Movimiento()
    {
        if (izquierda)
        {
            Turn.direccionbala = false;
            escala.localScale = new Vector3(-1, 1, 1);
            movimientoHorizintal = -velocidad;
            Turn.direccionbala = false;
        }
        else if (derecha)
        {
            Turn.direccionbala = true;
            escala.localScale = new Vector3(1, 1, 1);
            movimientoHorizintal = velocidad;
            Turn.direccionbala = true;
        }
        else
        {
            movimientoHorizintal = 0;
        }
    }
    public void SaltoBoton()
    {
        if (checkSalto)
        {
            anim.SetBool("Jump", true);
            arriba = true;
        }
    }
    public void SaltoBotonUp()
    {
        //anim.SetBool("Jump", false);
        arriba = false;
    }
    void Salto()
    {
        if (arriba)
        {
            rb.velocity = new Vector2(rb.position.x, potenciaSalto);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUpEmpuje")
        {
            Destroy(collision.gameObject);
            PowerUpBala.powerUpActiVerde = true;
            if (PowerUpBala.powerUpActiVerde)
            {
                balaCambiar.bullet = balaEmpuje.balaVerde;
                comprobar = true;
            }
        }
        if (collision.gameObject.tag == "PowerUpExplosion")
        {
            Destroy(collision.gameObject);
            PowerUpBala.powerUpActiveNegro = true;
            if (PowerUpBala.powerUpActiveNegro)
            {
                balaCambiar.bullet = balaNegra.balaNegra;
                comprobar = true;
            }
        }
        if (collision.gameObject.tag == "Antorcha")
        {
            antorcha.SetActive(enabled);
            Destroy(collision.gameObject);
            luz1.SetActive(!enabled);
        }

        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CajasFuturo")
        {
            transform.parent = null;
            colisioncajasfuturas = false;
        }
        if (collision.gameObject.tag == "Cinta")
        {
            cinto = false;
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "CajasFuturo")
        {
            transform.parent = collision.transform;
            colisioncajasfuturas = true;
        }

        if (collision.gameObject.tag == "BulletEnemy")
        {
            anim.SetTrigger("Hurt");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cinta")
        {
            cinto = true;
        }
    }
    void Power(GameObject balaPower)
    {
        if (!PowerUpBala.powerUpActiVerde)
        {
            comprobar = false;
            balaCambiar.bullet = balaPower;
            balaCambiar.numeroDisparos = 0;
        }
    }

}
