using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    
    string frase = "Hola de nuevo agente 001, una vez te encuentres en la isla encontrarás objetivos hostiles. " +
    "Acaba con ellos, tienes luz verde para abrir fuego si es necesario. Aunque conocemos tus dotes para resolver conflictos sin el uso de la violencia";
    public Text texto;
    private bool readText;
    public Animator animPressEscKey;
    public Text pressEscKey;
    public int count;
    public PausaConMovil pausaConMovil;
     
    public void Start()
    {
        StartCoroutine(Esperar());
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            readText = (true);
        }

        if (Input.anyKeyDown && readText == true)
        {
            count++;
            if (count == 1)
            {
                animPressEscKey.SetActive(true);
                pressEscKey.SetActive(true);
            }
            if (count == 2 )
            {
                pressEscKey.SetActive(false);
                Destroy(texto);
                pausaConMovil.movilAn.SetBool("PA", false);
            }            
        }
    }

    IEnumerator Esperar()
    {
        texto.text = texto.text;
        yield return new WaitForSeconds(1.25f); 

        foreach (char caracter in frase)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.07f);

            if (Input.anyKey)
            {
                texto.text = frase;
                yield break;                
            }
        }
    }
}
