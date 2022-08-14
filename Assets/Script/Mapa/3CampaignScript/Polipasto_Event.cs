using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polipasto_Event : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Subir o Bajar")]
    public bool bajar;

    [Header("Destino")]
    [HideInInspector]public bool llegarDestino;
    [SerializeField] Transform[] destinos;

    [Header("Velocidad")]
    public float velocidad;
    float cambiarDireccion;

    [Header("Cuerda Polipasto")]
    [SerializeField] SpriteRenderer cuerda;
    float cuerda_Size;

    [Header("Cantidad de Cajas Encima")]
    [HideInInspector]public int cajasEncima;
    public int cajasEncimaLimite = 2;

    RomperCarga pararAccion;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pararAccion = GetComponent<RomperCarga>();

        //Tamaño en el que crece la cuerda en base a la velocidad
        cuerda_Size = velocidad / 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ascensor_Event();
    }
    void Ascensor_Event()
    {
        //Una vez te cargues a uno de los dos el ascensor funcionara sin ninguna condicion de las cajas;
        if(!pararAccion.ascensorNormal)ActivarAscensor();
        //Abajo
        if (bajar)
        {
            //Dirigirse a la posicion al destino
            Vector3 movePosition = transform.position;
            movePosition.y = Mathf.MoveTowards(transform.position.y, destinos[0].position.y, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);

            //Cuando estes a cierto margen de distancia comienza a calcular para cambiar de destino.Esto se activara si te has cargado antes a uno de los dos cargueros
            if (pararAccion.ascensorNormal)
            {
                if (Vector2.Distance(destinos[0].position, transform.position) < 1) { cambiarDireccion += Time.deltaTime; if (cambiarDireccion >= 6) bajar = false; }
                else cambiarDireccion = 0;

            }

            //Mientras no hayas llegado a tu destino la cuerda aumentara
            if (destinos[0].position != transform.position) { cuerda.size += new Vector2(0f, cuerda_Size); llegarDestino = false; }
            else if (destinos[0].position == transform.position) llegarDestino = true;

        }
        //Arriba
        else
        {
            //Dirigirse a la posicion al destino
            Vector3 movePosition = transform.position;
            movePosition.y = Mathf.MoveTowards(transform.position.y, destinos[1].position.y, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);

            //Cuando estes a cierto margen de distancia comienza a calcular para cambiar de destino.Esto se activara si te has cargado antes a uno de los dos cargueros
            if (pararAccion.ascensorNormal)
            {
                if (Vector2.Distance(destinos[1].position, transform.position) < 1) { cambiarDireccion += Time.deltaTime; if (cambiarDireccion >= 6) bajar = true; }
                else cambiarDireccion = 0;
            }

            //Mientras no hayas llegado a tu destino la cuerda disminuira
            if (destinos[1].position != transform.position) { cuerda.size -= new Vector2(0f, cuerda_Size); llegarDestino = false; }
            else if (destinos[1].position == transform.position) llegarDestino = true;
        
        }
    }
    void ActivarAscensor()
    {
        if (cajasEncima >= cajasEncimaLimite) bajar = true;
        else if (cajasEncima <= 0) bajar = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Caja")) { cajasEncima++; collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Caja")) cajasEncima--;
    }


}
