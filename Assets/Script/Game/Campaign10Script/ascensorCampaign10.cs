using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ascensorCampaign10 : MonoBehaviour
{

    Transform ascensor;
    Rigidbody2D rb;
    public Transform piso1;
    public Transform piso0;
    public float velocidad;
    public bool subir = true;
    public bool bajar = false;
    public static float time = 2;
    public ascensorCampaign10 ascensorcampaign10;


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
        if (time < 2)
        {
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
        else
        {
            ascensorcampaign10.enabled = false;
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