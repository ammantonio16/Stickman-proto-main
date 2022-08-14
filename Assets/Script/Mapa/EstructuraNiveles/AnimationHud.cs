using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  
public class AnimationHud : MonoBehaviour
{
    //No tener que estar buscando constantemente el animator
    public static Animator detectar_echar;
    private void Awake()
    {
        detectar_echar = GameObject.Find("Detectar_Echar Event").GetComponent<Animator>();
    }
}
