using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaCarga : MonoBehaviour
{
    bool cargarAscensor;

    [Header("Caja Prefab")]
    public GameObject cajaRota;
    [SerializeField]List<GameObject> CajasCarro = new List<GameObject>();

    [Header("Tiempo Carga Ascensor")]
    public float cargarTiempoAscensorLimite;
    float cargarTiempoAscensor;

    [Header("Ubicaciones Colocar Cajas")]
    [SerializeField] Transform[] colocarCajasAscensor;
    int indexColocarCajas;

    [Header("Polipasto Estado")]
    public Polipasto_Event polipastoEstatus;

    [SerializeField] SoldadoNormal soldadoTimeCarga;
    int fueraTiempo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!soldadoTimeCarga.berserker && soldadoTimeCarga.GetComponent<SoldierLife>().vida > 0) CargarAscensor();
        FueraLimite();
        if (soldadoTimeCarga.berserker || soldadoTimeCarga.seeCadaverWarning) { cargarAscensor = false; soldadoTimeCarga.maxTiempoUbi = 5; }
    }

    void CargarAscensor()
    {
        //Si la plataforma esta arriba y ha llegado a su destino
        if (polipastoEstatus.llegarDestino && !polipastoEstatus.bajar)
        {
            if (cargarAscensor)
            {
                cargarTiempoAscensor += Time.deltaTime;
                if (cargarTiempoAscensor >= cargarTiempoAscensorLimite)
                {
                    if (CajasCarro.Count > 0 && polipastoEstatus.cajasEncima < 2)
                    {
                        //Destruye el elemento de la lista
                        CajaMasAlta();

                        //"Trasladar" cajas
                        GameObject CajaEsconderse = Instantiate(cajaRota, colocarCajasAscensor[indexColocarCajas].position, Quaternion.identity);
                        CajaEsconderse.transform.parent = colocarCajasAscensor[indexColocarCajas].transform;

                        //Si el indice es más pequeño que la lista, el index aumenta
                        if (indexColocarCajas < colocarCajasAscensor.Length) indexColocarCajas++;

                        //Restablece el tiempo para repetir cargar una caja
                        cargarTiempoAscensor = 0;
                    }



                }

            }

        }
        if (!cargarAscensor) {cargarTiempoAscensor = 0; indexColocarCajas = 0;}
        
    }
    void FueraLimite()
    {
        if (soldadoTimeCarga.siguienteUbi >= 90) fueraTiempo++;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Añade las cajas a una lista
        if (collision.gameObject.CompareTag("Caja")) CajasCarro.Add(collision.gameObject);

        //Una vez llega a la zona de descarga cambia el maxTiempoUbi, para esperar que suba el ascensor
        if (collision.gameObject.CompareTag("Enemy"))
        {
            cargarAscensor = true;


            if(fueraTiempo < 1) soldadoTimeCarga.maxTiempoUbi = 90;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Una vez el enemigo limpia la lista y dejas de cargar
        if (collision.gameObject.CompareTag("Enemy")) {cargarAscensor = false; CajasCarro.Clear();}

        //Restablece el tiempo a como esta establecido en la patrulla 
        if (collision.gameObject.CompareTag("Caja")) if (polipastoEstatus.cajasEncima >= 1 && polipastoEstatus.cajasEncima < 2) { soldadoTimeCarga.maxTiempoUbi = 5;}
    }

    void CajaMasAlta()
    {

        if (CajasCarro.Count > 1)
        {
            //Comprueba que caja esta más alta
            if (CajasCarro[0].GetComponent<Transform>().position.y > CajasCarro[1].GetComponent<Transform>().position.y)
            {
                Destroy(CajasCarro[0].gameObject);
                CajasCarro.Remove(CajasCarro[0]);
            }
            else
            {
                Destroy(CajasCarro[1].gameObject);
                CajasCarro.Remove(CajasCarro[1]);
            }
        }
        else 
        {
            Destroy(CajasCarro[0].gameObject);
            CajasCarro.Remove(CajasCarro[0]);
        }



    }
}
