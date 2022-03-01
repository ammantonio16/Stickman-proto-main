using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxC5 : MonoBehaviour
{

    string frase = "insertar texto historia en TextBoxC5 script";
    public Text texto;
    private bool readText;
    public Animator animPressEscKey;
    public Text pressEscKey;
    private int count;
    public PausaConMovil pausaConMovil;
    public GameObject player;

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
            if (count == 2)
            {
                player.gameObject.SetActive(true);
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
