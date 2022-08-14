using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutGasMask : MonoBehaviour
{
    [SerializeField] GameObject gas_Mask;
    [SerializeField] SaveButton mascaraButton;
    public string tagName;
    bool canPut;
    [HideInInspector] public bool putMask;


    void Update()
    {
        CanUseMask();
    }
    void CanUseMask()
    {
        if (canPut)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                putMask = true;
                PutMask();
            }
        }
    }
    void PutMask()
    {
        if (putMask)
        {
            gas_Mask.SetActive(true);
            mascaraButton.UsosGastados();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) canPut = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) canPut = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName)) canPut = false;
    }
}
