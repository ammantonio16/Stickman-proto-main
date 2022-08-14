using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargas : MonoBehaviour
{
    bool cargar;

    [Header("Caja Prefab")]
    public GameObject cajaRota;

    [Header("Tiempo Para Cargar Cajas Carretilla")]
    public float tiempoParaCargarLimite;
    float tiempoParaCargar;

    [Header("Ubicaciones Colocar Cajas")]
    [SerializeField]Transform[] spawnCajas;
    [SerializeField]int indexCajaSpawn;

    [SerializeField]SoldierLife soldadoCargarVida;
    void Start()
    {
        indexCajaSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {

        CargarMontacargas();

        if (soldadoCargarVida.GetComponent<SoldadoNormal>().berserker || soldadoCargarVida.GetComponent<SoldadoNormal>().seeCadaverWarning) cargar = false;

    }
    void CargarMontacargas()
    {
        //Si la distancia entre el player y enemigo es mayor a la establecida en el ataque, para evitar que cuando entre se dirija al coche y se empiecen a colocar cajas
        //que se acerque a la carretilla y la coja
        if(soldadoCargarVida.vida > 0)
        {
            if (cargar)
            {
                tiempoParaCargar += Time.deltaTime;
                if (tiempoParaCargar >= tiempoParaCargarLimite)
                {
                    //<<Cargas>> la carretilla
                    if (indexCajaSpawn < spawnCajas.Length)
                    {
                        GameObject boxRota = Instantiate(cajaRota, spawnCajas[indexCajaSpawn].position, Quaternion.identity);
                        boxRota.transform.parent = spawnCajas[indexCajaSpawn].transform;

                        //Si el indice es más pequeño que la lista, el index aumenta
                        indexCajaSpawn++;
                    }


                    //Restablece el tiempo para repetir cargar una caja
                    tiempoParaCargar = 0;

                    
                }
            }
            else { indexCajaSpawn = 0; tiempoParaCargar = 0; }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comienza a <<Cargar>>
        if (collision.gameObject.CompareTag("Enemy")) if (!soldadoCargarVida.GetComponent<SoldadoNormal>().berserker || !soldadoCargarVida.GetComponent<SoldadoNormal>().seeCadaverWarning) { cargar = true;}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Para de <<Cargar>>
        if (collision.gameObject.CompareTag("Enemy")) cargar = false;
    }
}
