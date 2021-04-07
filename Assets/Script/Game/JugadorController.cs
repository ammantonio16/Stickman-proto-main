using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public Animator anim;


    public WeaponController balaCambiar;
    public PowerUpBala balaEmpuje;
    public PowerUpBala balaNegra;
    public bool comprobar;
    public GameObject balaOriginal;
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
            if (Input.GetKeyDown(KeyCode.A)) 
            {
                derecha = true;
                //anim.SetBool("Walk", true);
            }
            if (Input.GetKeyDown(KeyCode.D)) 
            {
                izquierda = true;
                //anim.SetBool("Walk", true);
            }
            if (Input.GetKeyUp(KeyCode.A)) 
            {
                derecha = false;
                //anim.SetBool("Walk", false);
            }
            if (Input.GetKeyUp(KeyCode.D)) 
            {
                izquierda = false;
                //anim.SetBool("Walk", false);
            }
        }
    }

    public void IzquierdaBoton()
    {
        izquierda = true;
    }
    public void DerechaBoton()
    {
        derecha = true;
    }
    public void IzquierdaBotonUp()
    {
        izquierda = false;
    }
    public void DerechaBotonUp()
    {
        derecha = false;
    }
    private void FixedUpdate()
    {
        int bitmask = (1 << 9);
        RaycastHit2D IsGround;
        IsGround = Physics2D.Raycast(piernas.bounds.center, -Vector2.up, 0.45f, bitmask);
        Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.45f), Color.red);
        int cinta = (1 << 22);
        RaycastHit2D IsCinta;
        IsCinta = Physics2D.Raycast(piernas.bounds.center, -Vector2.up, 0.45f, cinta);
        Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.45f), Color.red);
        if (IsGround)
        {
            Salto();
            checkSalto = true;
            rb.velocity = new Vector2(movimientoHorizintal, rb.velocity.y);
            Debug.DrawRay(piernas.bounds.center, new Vector2(0f, -0.45f), Color.green);
        }
        if (!IsGround)
        {
            rb.velocity = new Vector2(movimientoHorizintal, rb.velocity.y);
            checkSalto = false;
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
            arriba = true;
        }
    }
    public void SaltoBotonUp()
    {
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
