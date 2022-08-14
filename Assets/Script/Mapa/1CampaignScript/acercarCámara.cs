using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class acercarCámara : MonoBehaviour
{
    public Camera mainCamera;
    bool activarAcercamineto;
    public float velocidadDeCamara;
    public Transform padre;
    public Transform hijo;
    public float dimension;
    private int count;
    //public TextBox textbox;
    

    void Start()
    {
        
    }

    void Update()
    {
        //if (textbox.count == 2)
        //{
            //activarAcercamineto = true;
        //}

        if (activarAcercamineto == true && count == 0) 
        {
            mainCamera.orthographicSize = dimension;

            Instantiate(hijo, padre);
            count++;

            //mainCamera.SetActive (false);
        }
        
    }
    
    void CamaraAcercar()
    {
        mainCamera.orthographicSize = mainCamera.orthographicSize - velocidadDeCamara;
    }

}
