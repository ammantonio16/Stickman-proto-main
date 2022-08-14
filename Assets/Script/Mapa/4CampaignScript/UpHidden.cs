using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpHidden : MonoBehaviour
{
    EscaleraMano hidden;
    bool imHere;

    [Header("Where do you want to go")]
    [SerializeField] Transform direction;
    [SerializeField] Image up_DownHidden;
    private void Awake()
    {
        hidden = FindObjectOfType<EscaleraMano>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHidden();
    }
    void MoveHidden()
    {
        if(hidden.imHidden && imHere)
        {
            ArrowUI();
            
            if (Input.GetKeyDown(KeyCode.Z)) hidden.GetComponent<Transform>().position = direction.position;
        }
        else if(!hidden.imHidden && !imHere) up_DownHidden.enabled = false;
    }
    void ArrowUI()
    {
        Vector3 distanceDoors;
        distanceDoors = direction.position - transform.position;

        up_DownHidden.enabled = true;

        //Where do you want arrow aimed
        if (distanceDoors.y < 0) up_DownHidden.rectTransform.rotation = new Quaternion(0f, 0f, 180f,0f); 
        else if(distanceDoors.y > 0) up_DownHidden.rectTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Escondite")) imHere = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Escondite")) imHere = false;
    }
}
