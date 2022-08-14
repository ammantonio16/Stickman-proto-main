using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AtraerAlarmZombie : MonoBehaviour
{
    CircleCollider2D sphereCollider;
    public float radioAtraccion;
    public float radioFrenar;
    public Transform sphereTransform;
    public float duracionAlarma;
    public static bool alarmaEncendida;
    public ZombieIA ia;

    bool atraction;

    public float patata;
    void Start()
    {
        sphereCollider = GetComponent<CircleCollider2D>();
        sphereTransform = GetComponent<Transform>();
        alarmaEncendida = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this != null)
        {
            if (duracionAlarma >= 20)
            {
                atraction = false;
                sphereCollider.radius -= 0.1f;
                if (sphereCollider.radius <= 0)
                {
                    sphereCollider.radius = 0f;
                    duracionAlarma = 0;
                    
                }
            }
            if (duracionAlarma < 20)
            {
                duracionAlarma += Time.deltaTime;
                patata = sphereCollider.radius += 0.1f;
                if (sphereCollider.radius >= radioAtraccion)
                {
                    atraction = true;
                    sphereCollider.radius = radioAtraccion;
                }
            }

        }
    }

    private void FixedUpdate()
    {
        //"AreaAtraccionSombis" es un área que tiene el objeto "SphereZombieFollow", este área se hace para que cada vez que un zombie entre en la zona se vea atraído hacía el objeto.
        //Esto se hace para simular la idea de que el zombie se vea atraído por el sonido, ya que se activa con la alarma del coche.
        int mascaraSombi = 1 << 26;
        Collider2D[] areaAtraccionSombis;
        areaAtraccionSombis = Physics2D.OverlapCircleAll(sphereTransform.position, patata, mascaraSombi);
        Collider2D[] areaFrenarSombis;
        areaFrenarSombis = Physics2D.OverlapCircleAll(sphereCollider.bounds.center, radioFrenar);
        //Cada vez que entre un "Collider2D" dentro del "AreaAtraccionSombis" ejecutará lo de dentro de las llaves
        if (atraction)
        {
            foreach (Collider2D sombi in areaAtraccionSombis)
            {

                if (sombi.gameObject.CompareTag("Sombis"))
                {
                    
                    sombi.gameObject.GetComponentInParent<ZombieIA>().atraccion = true;
                    Debug.Log("El estado de Atraccion sombi es:" + " " + sombi.gameObject.GetComponentInParent<ZombieIA>().atraccion);
                    if (sombi.gameObject.GetComponentInParent<ZombieIA>().atraccion)
                    {
                        Debug.Log("Sombi dirigite al vehículo más cercano");
                        sombi.gameObject.GetComponentInParent<ZombieIA>().alarmaCoche = true;
                        Vector3 atractionZombie = sombi.gameObject.GetComponentInParent<Transform>().position;
                        atractionZombie.x = Mathf.MoveTowards(sombi.gameObject.GetComponentInParent<Transform>().position.x, sphereTransform.position.x, 1f * Time.deltaTime);
                        sombi.gameObject.GetComponentInParent<Rigidbody2D>().MovePosition(atractionZombie);
                    }
                    if (!sombi.gameObject.GetComponentInParent<ZombieIA>().atraccion)
                    {
                        Vector3 atractionZombie = sombi.gameObject.GetComponentInParent<Transform>().position;
                        sombi.gameObject.GetComponentInParent<Rigidbody2D>().MovePosition(atractionZombie);
                    }
                    foreach (Collider2D sombiFrenado in areaFrenarSombis)
                    {
                        if (sombiFrenado.gameObject.CompareTag("Sombis"))
                        {
                            sombiFrenado.gameObject.GetComponentInParent<ZombieIA>().atraccion = false;
                        }
                    }
                    
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sombis")
        {
            Debug.Log("Hola he salido");
            collision.gameObject.GetComponentInParent<ZombieIA>().atraccion = false;
            collision.gameObject.GetComponentInParent<ZombieIA>().alarmaCoche = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = ia.atraccion ? Color.green : Color.red;
        Gizmos.DrawLine(sphereTransform.position,(Vector2)sphereTransform.position * 10f);
    }
}
