using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejillaTransporte : MonoBehaviour
{

    public GameObject botonInteractuar;
    public Transform rejillaArriba;
    public Transform rejillaAbajo;
    public Transform playerPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            botonInteractuar.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            botonInteractuar.SetActive(false);
        }
    }
    public void RejillaArribaAbajo()
    {
        playerPosition.position = rejillaAbajo.position;
    }
    public void RejillaAbajoArriba()
    {
        playerPosition.position = rejillaArriba.position;
    }
}
