using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NivelesMapa : MonoBehaviour
{
    //Variables Públicas
    #region
    [Header("Componentes dentro del Player")]
    public Rigidbody2D player;
    public Transform playerTrans;
    public Animator playerAnim;
    [Header("Movimiento Player")]
    public float velocidadPersonaje;
    [Header("Paneles Niveles")]
    public CambiarPanelGame cambiarNivel;
    public AccionSelectNivel activarSelectNivel;
    [Header("Ubicaciones Niveles")]
    public float distanciaEntrePuntos;
    public Transform[] posicionesNiveles;

    [Header("NO RELLENAR")]
    public GameObject idNivelChoque;
    #endregion
    //Variables Privadas
    #region
    // "i" representa el índice del array de "posicionesNiveles" que se usará para delimitar hasta que posiciones te puedes mover   
    public static int i;
    bool pasoEntreNiveles;
    #endregion

    void Start()
    {
        //Cuando cargue el mapa de nuevo, el Player aparecera en el nivel donde él ha decidido salir.
        playerTrans.position = posicionesNiveles[IDBotones.ubicacionNivel].position;

        Debug.Log(i);
        //Esta distancia se calcula para saber cuando el Player va a tener que usar los 
        distanciaEntrePuntos = posicionesNiveles[0].position.x - posicionesNiveles[1].position.x;
        cambiarNivel = FindObjectOfType<CambiarPanelGame>();
    }

    void Update()
    {
        Debug.Log(ControlDesbloqueoNiveles.numeroMovimientos + " " + "patata");
        MoverPlayerMapa();
        /*El calculo de si "distanciaEntrePuntos" es mayor o menor que cero, se hace simplementa por si se da el caso que un siguiente nivel al que el Player
          tiene que acceder esta en una posición detrás en el eje de las "x" y si ese es el caso, se invertiran los controles para que se adecúe al mapa.
          El mejor ejemplo son los niveles 6 y 7 cuando tu estes en el 6, avanzaras como siempre, pero como apartir del 7 los niveles se encuentran en la posición
          opuesta a la habitual se invertiran los controles, para que tenga más sentido a la hora de moverte.*/
        if (distanciaEntrePuntos < 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerTrans.localScale = new Vector3(2, 2, 2);
                //Si el Player se encuentra en un nivel se le permite avanzar o retroceder
                if (playerTrans.position == posicionesNiveles[i].position)
                {
                    //Esto es una condición con el "numeroMovimientos" para que detecte si puedes avanzar tras haberte pasado el nivel anterior y no accedas a los siguientes.
                    if (i < (posicionesNiveles.Length - 1) && i < ControlDesbloqueoNiveles.numeroMovimientos)
                    {
                        i++;
                    }
                }
                pasoEntreNiveles = true;

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerTrans.localScale = new Vector3(-2, 2, 2);
                if (playerTrans.position == posicionesNiveles[i].position)
                {
                    if (i > 0)
                    {
                        i--;
                    }
                }
                pasoEntreNiveles = true;

            }
        }
        if(distanciaEntrePuntos > 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerTrans.localScale = new Vector3(2, 2, 2);
                
                if (playerTrans.position == posicionesNiveles[i].position)
                {
                    if (i > 0)
                    {
                        i--;
                    }
                }
                pasoEntreNiveles = true;

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerTrans.localScale = new Vector3(-2, 2, 2);
                if (playerTrans.position == posicionesNiveles[i].position)
                {
                    if (i < (posicionesNiveles.Length - 1) && i < ControlDesbloqueoNiveles.numeroMovimientos)
                    {
                        i++;
                    }
                }
                pasoEntreNiveles = true;

            }
        }
    }
    //El movimiento del Player exclusivo del selector de mapa
    void MoverPlayerMapa()
    {
        /*Cuando "pasoEntreNiveles" sea verdadero podrás volver a darle al boton de avanzar o retroceder. El bool se hace para que en mitad del recorrido
         el jugador si le da más de una vez al boton de avanzar o retroceder no vaya dos posiciones por delante en el mapa, porque sino rompería el esquema.*/
        if (pasoEntreNiveles)
        {
            //El movimiento simplemente consiste en darle a "A" o "D" para que el Player se dirigiga a las diferentes ubicaciones en "posicionesNiveles".
            //Las ubicaciones se deben colocar correctamente para que funcione el movimiento bien.
            player.MovePosition(Vector2.MoveTowards(player.position, posicionesNiveles[i].position, velocidadPersonaje * Time.deltaTime));
            playerAnim.SetBool("Walk", true);

            //Calcula la distancia entre tu punto anterior con la actual, excepto en el primer nivel porque esa condición es imposible de que ocurra.
            if (i > 0)
            {
                distanciaEntrePuntos = posicionesNiveles[i - 1].position.x - posicionesNiveles[i].position.x;
            }
            if(i <= 0)
            {
                distanciaEntrePuntos = posicionesNiveles[0].position.x - posicionesNiveles[1].position.x;
            }

            //Esto hace que si el Player esta posicionado en algún nivel tenga permitido la acción de "activarSelectNivel", que le permitira acceder al panel para ver y tener la posibilidad de entrar al nivel.
            //Y si no esta posicionado en ningún nivel no puede acceder al "activarSelectNivel".
            if (playerTrans.position == posicionesNiveles[i].position)
            {
                activarSelectNivel.enabled = true;
                playerAnim.SetBool("Walk", false);
                pasoEntreNiveles = false;
            }
            
            if (playerTrans.position != posicionesNiveles[i].position)
            {
                activarSelectNivel.enabled = false;
            }
        }
        //Si "pasoEntreNiveles" es falso, quiere decir que has llegado al nivel.
        else
        {
            Debug.Log("He llegado");
        }
        
    }
    /*La primera acción: es la de coger el objeto con el que choca, el punto, y recoger su "Id" del script "IDBotones" para que en el script "AccionSelectNivel",
      cuando le des a "Space" por segunda vez te lleve a la escena del correspondiente nivel en donde el Player se encuentra.
      La segunda acción: cambia la Imagen, el Título y la Descripción del nivel según con que punto el Player colisione.*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Nivel")
        {
            //Primera Acción:
            Debug.Log(AccionSelectNivel.idNivel);
            idNivelChoque = collision.gameObject;
            AccionSelectNivel.idNivel = idNivelChoque.GetComponent<IDBotones>().iD;
            //Segunda Acción:
            cambiarNivel.tituloNivel.text = cambiarNivel.titulosTodosNiveles[i].text;
            cambiarNivel.imagenDelNivel.sprite = cambiarNivel.imagenTodosNiveles[i].sprite;
            cambiarNivel.descripcionDeNiveles.text = cambiarNivel.descripcionTodosNiveles[i].text;
        }
    }

}
