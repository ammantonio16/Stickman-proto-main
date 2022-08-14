using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDescarga : MonoBehaviour
{

    List<GameObject> cajasAscensor = new List<GameObject>();

    [Header("Tiempo Descargar Cajas")]
    public float descargarTiempoAscensorLimite;
    float descargarTiempoAscensor;
    bool descargarAscensor;

    [Header("Polipasto Estado")]
    public Polipasto_Event polipastoEstado;

    [SerializeField]SoldadoNormal soldadoTimeDescarga;

    //Fuera del limite
    int fueraLimite;
    void Start()
    {
        //NOTA:Hacer que cuando uno de los dos muera el ascensor suba y baje normal; Seguidamente cuando pase ese minuto y medio el tio de arriba o abajo se extrañaran harán un llamado general y todos estaran alertas
        //esto no pasara si ambos esta KO;
    }

    // Update is called once per frame
    void Update()
    {
        if(soldadoTimeDescarga.GetComponent<SoldierLife>().vida > 0) DescargarAscensor();

        //En cuanto uno de los 2 muera una
        TiempoMaximoEspera();

        if (soldadoTimeDescarga.berserker || soldadoTimeDescarga.seeCadaverWarning) {descargarAscensor = false; soldadoTimeDescarga.maxTiempoUbi = 5; }
    }
    void DescargarAscensor()
    {
        //NOTA; Mantener ubicaciones para instanciar particulas y simular que coloca las cajas;

        //Si la plataforma esta abajo y ha llegado a su destino
        if (polipastoEstado.llegarDestino && polipastoEstado.bajar)
        {
            if (descargarAscensor && !soldadoTimeDescarga.berserker)
            {
                descargarTiempoAscensor += Time.deltaTime;
                if (descargarTiempoAscensor >= descargarTiempoAscensorLimite)
                {
                    //Destruye el elemento de la lista
                    if (cajasAscensor.Count > 0) { Destroy(cajasAscensor[0].gameObject); cajasAscensor.Remove(cajasAscensor[0]); }


                    //Restablece el tiempo para repetir quitar una caja
                    descargarTiempoAscensor = 0;

                    //Quita una caja al total
                    polipastoEstado.cajasEncima--;
                }
            }
        }

        //Resetea los valores fuera de la primera condicion
        if(!descargarAscensor) descargarTiempoAscensor = 0;
    }
    void TiempoMaximoEspera()
    {
        if (soldadoTimeDescarga.siguienteUbi >= 90) fueraLimite++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Añade las cajas a una lista
        if (collision.gameObject.CompareTag("Caja")) cajasAscensor.Add(collision.gameObject);

        //Una vez llega a la zona de descarga cambia el maxTiempoUbi, para esperar que baje el ascensor
        //Activa la accion de quitar las cajas
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            descargarAscensor = true;

            //Si todavia estan vivos los dos establecelo en 90;
            if (fueraLimite < 1) soldadoTimeDescarga.maxTiempoUbi = 90;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Desactiva la accion de quitar cajas y restablece sus valores
        if (collision.gameObject.CompareTag("Enemy")) { descargarAscensor = false; }

        //El trigger es más lento que el update, por tanto cuando quede <<1 caja>>, realmente sera 0 en el trigger 
        if (collision.gameObject.CompareTag("Caja")) if(polipastoEstado.cajasEncima <= 1) soldadoTimeDescarga.maxTiempoUbi = 5;
    }
}
