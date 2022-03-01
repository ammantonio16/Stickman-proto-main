using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzleArbolManager : MonoBehaviour
{
    public string codigoCorrecto = "102";
    public string playerCode = "";

    public static int totalDigitos = 0;

    public SpriteRenderer[] botones;

    public GameObject[] pasadizo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(totalDigitos == 3)
        {
            if(playerCode == codigoCorrecto)
            {
                Debug.Log("Respuesta Correcta");
                pasadizo[0].SetActive(false);
                pasadizo[1].SetActive(false);
            }
            else
            {
                totalDigitos = 0;
                playerCode = "";
                Debug.Log("Código Incorrect, vuelva a intentarlo");
                botones[0].color = new Color(255f, 0f, 0f, 255f);
                botones[1].color = new Color(255f, 0f, 0f, 255f);
                botones[2].color = new Color(255f, 0f, 0f, 255f);
            }
        }
    }
    public void Asignar(int number)
    {
        botones[0].color = new Color(0f, 255f, 0f, 255f);
        playerCode += number.ToString();
        totalDigitos += 1;
    }
    public void Asignar1(int number)
    {
        botones[1].color = new Color(0f, 255f, 0f, 255f);
        playerCode += number.ToString();
        totalDigitos += 1;
    }
    public void Asignar2(int number)
    {
        botones[2].color = new Color(0f, 255f, 0f, 255f);
        playerCode += number.ToString();
        totalDigitos += 1;
    }
}
