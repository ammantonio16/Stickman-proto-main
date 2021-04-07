using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTriggerAscensor : MonoBehaviour
{
    public Animator ascensor;
    public GameObject botonSalto;
    public GameObject botonInteractuar;
    public GameObject plataformaIsla;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ascensor menu")
        {
            ascensor.SetBool("menu", true);
            
        }
        if(collision.gameObject.tag == "Lancha")
        {
            botonInteractuar.SetActive(true);
            botonSalto.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ascensor menu")
        {
            ascensor.SetBool("menu", false);
        }
        if (collision.gameObject.tag == "Lancha")
        {
            botonInteractuar.SetActive(false);
            botonSalto.SetActive(true);
        }
    }
    public void InteraccionIsla()
    {
        SceneManager.LoadScene(2);
    }
}
