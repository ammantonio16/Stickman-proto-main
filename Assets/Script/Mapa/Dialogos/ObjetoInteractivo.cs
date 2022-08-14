using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    public Textos textos;
    public void DetenerDialogo()
    {
        FindObjectOfType<ControlDialogos>().ActivarCartel(textos);
    }
}
