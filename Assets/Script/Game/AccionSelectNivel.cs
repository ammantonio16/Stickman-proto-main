using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccionSelectNivel : MonoBehaviour
{
    //Variables Públicas
    #region
    [Header("Características del Movimiento de la Cámara")]
    public Camera mainCamera;
    public float acercamiento;
    public float alejamiento;
    /*"puntoBlanco" sirve para que la cámara haga focus a ese punto cuando el Player de por primera vez al "Space".
       De esta manera se logra que todo el canvas se coloque en esa distribución*/
    public Transform puntoBlaco;
    public static bool action;
    #endregion
    //Variables Privadas
    #region
    [Header("Acceder Movimiento del Player")]
    NivelesMapa enableMover;
    Transform myPlayer;
    CambiarPanelGame activarPanel;
    public static int idNivel;
    //Los contadores se usan para evitar que se repitan los métodos(o acciones) al presionar varias veces una misma tecla. 
    int contador;
    int contadorScenas;
    #endregion
    void Start()
    {
        myPlayer = GetComponent<Transform>();
        activarPanel = FindObjectOfType<CambiarPanelGame>();
        enableMover = FindObjectOfType<NivelesMapa>();
        acercamiento = 9;
        action = false;
    }

    void Update()
    {
        EscalaCanvas();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            acercamiento = 9;
            Debug.Log(action);
            if (contadorScenas <= 0)
            {
                action = true;
                contadorScenas++;
            }
            /*Si "contadorScenas" es mayor que 0, quiere decir que estas en el canvas del nivel y por lo tanto si le das
              de nuevo a "Space" accederás al nivel correspondiente.*/
            else 
            {
                SceneManager.LoadScene(idNivel);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            contadorScenas = 0;
            contador = 0;
            action = false;
        }
        //Si "action" es true, se abre el canvas del nivel y bloquea el movimiento del Player inhabilitando su script "NivelesMapa"
        if (action)
        {
            activarPanel.panelGame.SetActive(true);
            enableMover.enabled = false;
            if(contador <= 0)
            {
                AcercarCamara();
                contador++;
            }
        }
        //Si "action" es falso, el canvas del nivel se cerrará y el Player podrá moverse de nuevo, activando el script "NivelesMapa"
        else
        {
            activarPanel.panelGame.SetActive(false);
            enableMover.enabled = true;
            AlejarCamara();
        }

    }

    //"AcercarCamara" acerca la cámara cuando el Player presiona "Space".
    public void AcercarCamara()
    {
        mainCamera.orthographicSize = acercamiento;
    }

    //"AlejarCamara" retrocede la cámara cuando el Player presiona "Space".
    public void AlejarCamara()
    {
        mainCamera.orthographicSize = alejamiento;
    }

    //"EscalaCanvas" se utiliza para que el canvas se coloque siempre en la posición correcta independientemente de hacia donde este mirando el Player
    void EscalaCanvas()
    {
        if(myPlayer.localScale.x == 2)
        {
            puntoBlaco.localPosition = new Vector3(4.96f, 0f, puntoBlaco.position.z);
        }
        if (myPlayer.localScale.x == -2)
        {
            puntoBlaco.localPosition = new Vector3(-4.96f, 0f, puntoBlaco.position.z);
        }
    }
}
