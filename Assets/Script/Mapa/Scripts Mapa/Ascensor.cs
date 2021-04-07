using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Ascensor : MonoBehaviour
{

    Transform ascensor;
    Rigidbody2D rb;
    public Transform piso1;
    public Transform piso0;
    public float velocidad;
    public bool subir = true;
    public bool bajar = false;
    public int time = 0;
    Ascensor ac;
    
    void Start()
    {
        ascensor = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoAscensor();
    }
    void MovimientoAscensor()
    {
        float step = velocidad * Time.deltaTime;

            if (subir)
            {
                ascensor.position = Vector2.MoveTowards(ascensor.position, piso1.position, step);
                if (Vector2.Distance(ascensor.position, piso1.position) <= 0)
                {
                    time++;
                    StartCoroutine("BajarSubir", 2f);
                }
            }
            if (bajar)
            {
                ascensor.position = Vector2.MoveTowards(ascensor.position, piso0.position, step);
                if (Vector2.Distance(ascensor.position, piso0.position) <= 0)
                {
                    time++;
                    StartCoroutine("SubirBajar", 2f);
                    
                }
            }
        
    }
    IEnumerator BajarSubir(float time)
    {
        subir = false;
        yield return new WaitForSeconds(time);
        bajar = true;
    }
    IEnumerator SubirBajar(float time)
    {
        bajar = false;
        yield return new WaitForSeconds(time);
        subir = true;
    }

}
