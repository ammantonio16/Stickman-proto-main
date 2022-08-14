using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlDialogos : MonoBehaviour
{
    Animator animDialogo;
    private Queue <string> colaDialogo = new Queue<string>();
    Textos texto;
    [SerializeField] TextMeshProUGUI textPantalla;
    private void Awake()
    {
        animDialogo = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivarCartel(Textos textObjeto)
    {
        animDialogo.SetBool("Dialogo_Event", true);

        texto = textObjeto;
    }
    public void ActivarTexto()
    {
        colaDialogo.Clear();
        foreach(string textoGuardar in texto.arrayTextos)
        {
            colaDialogo.Enqueue(textoGuardar);
        }
        SiguienteFase();

    }
    public void SiguienteFase()
    {
        if(colaDialogo.Count == 0)
        {
            CierraBocadillo();
            return;
        }
        string fraseActual = colaDialogo.Dequeue();
        textPantalla.text = fraseActual;
        StartCoroutine(MostrarCaracteres(fraseActual));

    }
    void CierraBocadillo()
    {
        animDialogo.SetBool("Dialogo_Event", false);
    }
    IEnumerator MostrarCaracteres(string textoAMostrar)
    {
        textPantalla.text = "";
        foreach(char caracter in textoAMostrar.ToCharArray())
        {
            textPantalla.text += caracter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
