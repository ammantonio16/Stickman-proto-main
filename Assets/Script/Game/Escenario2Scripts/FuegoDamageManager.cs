using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoDamageManager : MonoBehaviour
{
    bool q;
    int s;
    float m;
    float n;
    GameObject f;
    GameObject fC;
    Transform mP;
    Life2Enemy v;

    // Start is called before the first frame update
    void Start()
    {
        FuegoDañoB(q, s, m, n, f, fC, mP, v);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void FuegoDañoB(bool activarQuemar, int numeroQuemaduras, float duracionQuemaduras, float dañoEntreQuemaduras, GameObject fuego, GameObject fuegoClon, Transform ubicacionQuemaduras, Life2Enemy vida)
    {

        if (activarQuemar)
        {
            duracionQuemaduras += Time.deltaTime;
            
        }
    }
    public static void FuegoDaño(bool activarQuemar, int numeroQuemaduras, float duracionQuemaduras, float dañoEntreQuemaduras, GameObject fuego, GameObject fuegoClon, Transform ubicacionQuemaduras, Life2Enemy vida)
    {
        if (activarQuemar)
        {
            duracionQuemaduras += Time.deltaTime;
            if (numeroQuemaduras < 1)
            {
                Debug.Log("Se ha instanciado el fuego bien Sombi");
                fuegoClon = Instantiate(fuego, ubicacionQuemaduras.transform);
                numeroQuemaduras++;
            }
            if (fuegoClon != null && Time.time > dañoEntreQuemaduras + 2f)
            {
                vida.VidaBaja(10);
                dañoEntreQuemaduras = Time.time;
                Debug.Log("El fuego existe Sombi");
            }
            if (duracionQuemaduras >= 10)
            {
                activarQuemar = false;
                Destroy(fuegoClon);
                numeroQuemaduras = 0;
                duracionQuemaduras = 0;
                dañoEntreQuemaduras = Time.time;
            }
        }
    }
    public static void FuegoDañoSombi(bool activarQuemar, int numeroQuemaduras, float duracionQuemaduras, float dañoEntreQuemaduras, GameObject fuego, GameObject fuegoClon, Transform ubicacionQuemaduras, ZombieLife vidaSombi)
    {
        if (activarQuemar)
        {
            duracionQuemaduras += Time.deltaTime;
            if (numeroQuemaduras < 1)
            {
                Debug.Log("Se ha instanciado el fuego bien Sombi");
                fuegoClon = Instantiate(fuego, ubicacionQuemaduras.transform);
                numeroQuemaduras++;
            }
            if (fuegoClon != null && Time.time > dañoEntreQuemaduras + 2f)
            {
                vidaSombi.DañoRecibidoZombie(10);
                dañoEntreQuemaduras = Time.time;
                Debug.Log("El fuego existe Sombi");
            }
            if (duracionQuemaduras >= 10)
            {
                activarQuemar = false;
                Destroy(fuegoClon);
                numeroQuemaduras = 0;
                duracionQuemaduras = 0;
                dañoEntreQuemaduras = Time.time;
            }
        }
    }
}
