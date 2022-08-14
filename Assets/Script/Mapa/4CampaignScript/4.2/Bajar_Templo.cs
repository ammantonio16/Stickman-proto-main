using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bajar_Templo : MonoBehaviour
{
    bool estoyHere;
    EscaleraMano hidden;

    [SerializeField] Image arrow;
    [SerializeField] Transform direction;

    public int checkpointNextScene;
    public int nextScene;

    ActivarModificador active;
    private void Awake()
    {
        active = GetComponent<ActivarModificador>();
        hidden = FindObjectOfType<EscaleraMano>();
    }

    // Update is called once per frame
    void Update()
    {
        if(active.modificacion) BajarOculto();
    }

    void BajarOculto()
    {
        if (hidden.imHidden && estoyHere)
        {
            ArrowUI();
            //Colocar en la cuerda 

            if (Input.GetKeyDown(KeyCode.Z))
            {
                CheckPointController.numeroCheckPoint = checkpointNextScene;
                EstructuraNiveles.nivel = nextScene;
                AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
            }
        }
    }
    void ArrowUI()
    {
        Vector3 distanceDoors;
        distanceDoors = direction.position - transform.position;

        arrow.enabled = true;

        //Where do you want arrow aimed
        if (distanceDoors.y < 0) arrow.rectTransform.rotation = new Quaternion(0f, 0f, 180f, 0f);
        else if (distanceDoors.y > 0) arrow.rectTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Escondite")) estoyHere = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Escondite")) estoyHere = false;
    }
}