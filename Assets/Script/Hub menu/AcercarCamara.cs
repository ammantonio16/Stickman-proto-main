using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcercarCamara : MonoBehaviour
{
    public Camera mainCamera;
    bool encenderCamaraAcercamiento;
    public float velocidadDeCamara;
    public Animator fundidoANegro;
    public int numeroScena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (encenderCamaraAcercamiento)
        {
            StartCoroutine("Transicion");
            if (mainCamera.orthographicSize <= 3.5)
            {
                fundidoANegro.SetBool("Repetir", true);
            }
            if (mainCamera.orthographicSize <= 2)
            {
                StopCoroutine("Transicion");
                SceneManager.LoadScene(numeroScena);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            encenderCamaraAcercamiento = true;
        }
    }
    IEnumerator Transicion()
    {
        yield return new WaitForSeconds(1f);
        CamaraAcercar();

    }
    void CamaraAcercar()
    {
        mainCamera.orthographicSize = mainCamera.orthographicSize - velocidadDeCamara;
    }
    
}
