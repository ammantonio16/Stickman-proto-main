using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] //= No esta asociado a nada, simplemente sera llamado por los otros
public class Textos
{
    [TextArea(1, 6)]
    public string[] arrayTextos;

}
