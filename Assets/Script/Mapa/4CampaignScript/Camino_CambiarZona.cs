using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camino_CambiarZona : MonoBehaviour
{


    public int siguienteEscena;
    public int checkpointSpanwScene;

    bool camino;

    GameObject childIcono;
    [SerializeField] Transform crearIcono;
    public GameObject iconoFlecha;

    int putIcon;


    private void Update()
    {
        Caminar();
        
        if (childIcono != null)
        {
            childIcono.transform.parent = crearIcono;
        }
    }
    // Update is called once per frame
    public void Caminar()
    {
        if (camino)
        {
            if (putIcon < 1) 
            {
                childIcono = Instantiate(iconoFlecha, crearIcono.position, Quaternion.identity);
                putIcon++;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                CheckPointController.numeroCheckPoint = checkpointSpanwScene;
                EstructuraNiveles.nivel = siguienteEscena;
                AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
            }
        }
        else
        {
            putIcon = 0;
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            camino = true;
        }
        
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            camino = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { Destroy(childIcono); camino = false; } 
    }
}
