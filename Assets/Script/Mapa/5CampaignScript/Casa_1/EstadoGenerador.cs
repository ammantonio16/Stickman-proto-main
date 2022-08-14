using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoGenerador : MonoBehaviour
{
    public int indexModificador;
    public int indexLampara;
    [SerializeField] GameObject luz;
    [SerializeField] GameObject agua;
    [HideInInspector] public bool on;
    bool imHereInGen;
    bool imEnemy;
    float timeOnEnemy;

    [Header("Vida Personajes")]
    [SerializeField] VidaPlayer vidaJugador;
    [SerializeField] SoldierLife soldadoVida;

    public enum StateGenerator
    {
        normal,
        mojado
    }
    public StateGenerator stateGenerator;


    //Cabreo Enemigo
    int numeroVecesUsados;
    float resetearTiempo;
    bool comenzarCuenta;

    ActivarModificador activar;

    private void Awake()
    {
        activar = GetComponent<ActivarModificador>();
    }
    private void Start()
    {
        if (!StatusGameobjectsVariables.statusGameobject.modificacion[indexModificador].modificacion) 
        {
            stateGenerator = StateGenerator.normal;
            on = true;
        }
        else
        {
            stateGenerator = StateGenerator.mojado;
            on = false;
            agua.SetActive(true);
        }
        

    }
    private void Update()
    {
        GeneratorFuncion();
        if (activar.modificacion)
        {
            agua.SetActive(true);
            stateGenerator = StateGenerator.mojado;
            on = false;
            StatusGameobjectsVariables.statusGameobject.modificacion[indexModificador].modificacion = true;
        }
    }
    void GeneratorFuncion()
    {
        switch (stateGenerator)
        {
            case StateGenerator.normal:
                //Si entra el enemigo hacer que pase un tiempo y encienda
                if (imHereInGen)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        on = !on;
                        numeroVecesUsados++;
                        comenzarCuenta = true;
                        resetearTiempo = 0;
                    }

                    CabreoEnemigo();

                }
                if (imEnemy)
                {
                    timeOnEnemy += Time.deltaTime;
                    if(timeOnEnemy >= 1)
                    {
                        on = true;
                    }
                }

                //If there is light, you can on or off the lamp
                if(luz != null)
                {
                    if (on) luz.SetActive(true);
                    else luz.SetActive(false);
                }

                break;
            case StateGenerator.mojado:
                if (imHereInGen)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        vidaJugador.Daño(150);
                        //Instanciar explosion o efecto de descarga
                    }
                }
                if (imEnemy)
                {
                    timeOnEnemy += Time.deltaTime;
                    if (timeOnEnemy >= 1)
                    {
                        if(soldadoVida.vida > 0)
                        {
                            soldadoVida.Daño(3);
                        }
                    }
                }

                if (!on) 
                {
                    //Si el objeto esta encendido y la lampara no esta rota // quiza buscar otra manera dado que si caes la tabla del sotano se rompe
                    if (!StatusGameobjectsVariables.statusGameobject.modificacion[indexLampara].modificacion)
                    {
                        luz.SetActive(false);
                    }
                }

                break;
        }
    }

    void CabreoEnemigo()
    {
        if (comenzarCuenta)
        {
            resetearTiempo += Time.deltaTime;
            if (resetearTiempo >= 4)
            {
                numeroVecesUsados = 0;
                comenzarCuenta = false;
                resetearTiempo = 0;
            }
            if (numeroVecesUsados >= 20)
            {
                soldadoVida.GetComponent<SoldadoNormal>().berserker = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) imHereInGen = true;
        if (collision.gameObject.CompareTag("Enemy")) imEnemy = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) imHereInGen = false;
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            imEnemy = false;
            timeOnEnemy = 0;
        }
    }
}
