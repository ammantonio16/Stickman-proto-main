using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polipasto_Normal : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;

    [Header("Subir o Bajar")]
    public bool bajar;

    [Header("Destino")]
    public Transform[] destinos;

    [Header("Velocidad")]
    public float velocidad;
    float cambiarDireccion;
    public float cambiarDireccionTimeLimite;

    [Header("Cuerda Polipasto")]
    [SerializeField] SpriteRenderer cuerda;
    float cuerda_Size;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        cuerda_Size = velocidad / 10;

        bajar = true;
    }
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        Ascensor_Normal();
    }
    void Ascensor_Normal()
    {
        //Una vez te cargues a uno de los dos el ascensor funcionara sin ninguna condicion de las cajas;
        //Abajo
        if (bajar)
        {
            //Dirigirse a la posicion al destino
            Vector3 movePosition = transform.position;
            movePosition.y = Mathf.MoveTowards(transform.position.y, destinos[0].position.y, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);

            //Cuando estes a cierto margen de distancia comienza a calcular para cambiar de destino.Esto se activara si te has cargado antes a uno de los dos cargueros
            if (Vector2.Distance(destinos[0].position, transform.position) < 1) { cambiarDireccion += Time.deltaTime; if (cambiarDireccion >= cambiarDireccionTimeLimite) bajar = false; }
            else cambiarDireccion = 0;

            //Mientras no hayas llegado a tu destino la cuerda aumentara
            if (transform.position != destinos[0].position) { cuerda.size += new Vector2(0f, cuerda_Size); }
            

        }
        //Arriba
        else
        {
            //Dirigirse a la posicion al destino
            Vector3 movePosition = transform.position;
            movePosition.y = Mathf.MoveTowards(transform.position.y, destinos[1].position.y, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);

            //Cuando estes a cierto margen de distancia comienza a calcular para cambiar de destino.Esto se activara si te has cargado antes a uno de los dos cargueros
            if (Vector2.Distance(destinos[1].position, transform.position) < 1) { cambiarDireccion += Time.deltaTime; if (cambiarDireccion >= cambiarDireccionTimeLimite) bajar = true; }
            else cambiarDireccion = 0;


            //Mientras no hayas llegado a tu destino la cuerda disminuira
            if (transform.position != destinos[1].position) { cuerda.size -= new Vector2(0f, cuerda_Size);}
            

        }
    }
}
