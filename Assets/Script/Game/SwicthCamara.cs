using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwicthCamara : MonoBehaviour
{
    public Camera mainCamera;
    public Camera segundaCamera;
    public bool encender;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Alejar()
    {
        segundaCamera.SetActive(false);
        mainCamera.SetActive(true);
    }
    public void Acercar()
    {
        segundaCamera.SetActive(true);
        mainCamera.SetActive(false);
    }
    public void SwitchCamaras()
    {
        encender = !encender;
        if (!encender)
        {
            Acercar();
        }
        else
        {
            Alejar();
        }
    }
    private void OnMouseDrag()
    {
        mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if(mainCamera.ScreenToWorldPoint(Input.mousePosition) != null)
        {
            Debug.Log("Has arrastrado pa");
        }
        mainCamera.transform.position += new Vector3(1f, 1f, 0f);
    }
    private void OnMouseDown()
    {
        Debug.Log("Has arrastrado");
    }
}
