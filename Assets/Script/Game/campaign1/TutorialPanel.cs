using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public Animator anim;
    string frase = "Tutorial";
    public Text texto;

    public void Start()
    {
        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        foreach (char caracter in frase)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.3f);
        }
        
    }
}
