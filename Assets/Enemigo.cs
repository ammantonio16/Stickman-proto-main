using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemigo : MonoBehaviour
{
    [Header("Movimiento")]
    public float enemigoSpeed;
    public Transform checkGround;//Rayo para ver visualmente si toca suelo
    public Transform checksalto;//Rayo para ver si puedes saltar o no
    public Transform checkpared;//Rayo que te permite detectar la altura
    public Transform enemigo;
    Rigidbody2D rb; //El como se va a mover el enemigo
    public BoxCollider2D boxCuerpoPlayer;//El punto origen para saber la distancia que guarda con el enemigo
    public float fuerzaSalto;
    public float pruebaVelocidad;
    public bool escalaEnemigo;

    


    [Header("Detector y Apuntado")]
    public BoxCollider2D boxCuerpo;
    public float deteccion;
    public float radioDisparo;
    public float radioPlayer;
    public GameObject mano;
    public GameObject target;

    [Header("Shoot")]
    public GameObject balaEnemigo;
    public Transform spawnBala;
    public Rigidbody2D cuerpoBala;
    public float fuerzaBala;
    public float contador;
    public int municion;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        int bitmask = (1 << 9);
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

        if (IsGrounded)
        {
            Escala();
            Debug.Log("He tocado Tierra");
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
                    int shootmask = (1 << 20);
                    Collider2D areadeteccion;
                    areadeteccion = Physics2D.OverlapCircle(boxCuerpo.bounds.center, deteccion, shootmask);
                    //Cuendo detecta a "player"
                    if (areadeteccion)
                    {
                        SeguirPlayer();
                    }
                    if (!areadeteccion)
                    {
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
                    int shootmask = (1 << 20);
                    Collider2D areadeteccion;
                    areadeteccion = Physics2D.OverlapCircle(boxCuerpo.bounds.center, deteccion, shootmask);
                    //Cuendo detecta a "player"
                    if (areadeteccion)
                    {
                        SeguirPlayer();
                    }
                    if (!areadeteccion)
                    {
                        mano.transform.rotation = Quaternion.Euler(0, 0, 0);
                        municion = 0;
                    }
                }
            }
        }

    }
    void SeguirPlayer()
    {
        //Dirección a la que va el enemigo: el "player"
        Vector3 movePosition = enemigo.position;
        movePosition.x = Mathf.MoveTowards(transform.position.x, target.transform.position.x, enemigoSpeed * Time.deltaTime);
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
        areaPlayer = Physics2D.OverlapCircle(boxCuerpoPlayer.bounds.center, radioPlayer, arealimite);
        Collider2D areaDisparo;
        areaDisparo = Physics2D.OverlapCircle(boxCuerpoPlayer.bounds.center, radioDisparo, arealimite);
        if (areaPlayer)
        {
            movePosition = enemigo.position;
            rb.MovePosition(movePosition);
        }
        if (areaDisparo)
        {
            Apuntar();
        }
    }
    void Apuntar()
    {
        float AngleRad = Mathf.Atan2(target.transform.position.y - mano.transform.position.y, target.transform.position.x - mano.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        mano.transform.rotation = Quaternion.Euler(0, 0, 90 + AngleDeg);
        Debug.DrawRay(spawnBala.position, (target.transform.position - mano.transform.position), Color.blue);
        if (municion < 1)
        {
            GameObject balaEnemigoclon = Instantiate(balaEnemigo, spawnBala.position, balaEnemigo.transform.rotation);
            fuerzaBala = Random.Range(10, 40);

            if (escalaEnemigo)
            {
                balaEnemigoclon.GetComponent<Rigidbody2D>().AddForce(spawnBala.right * 100 * fuerzaBala);
                municion++;
            }
            if (!escalaEnemigo)
            {
                balaEnemigoclon.GetComponent<Rigidbody2D>().AddForce(-spawnBala.right * 100 * fuerzaBala);
                municion++;
            }
        }
    }
    void Escala()
    {
        if (target.transform.position.x > enemigo.transform.position.x)
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
}

