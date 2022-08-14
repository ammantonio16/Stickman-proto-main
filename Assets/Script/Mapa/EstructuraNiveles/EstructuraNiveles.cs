using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EstructuraNiveles
{
    public static int nivel;


    public void Nivel_1(bool objetivoCompletado, GameObject salidaNivel)
    {
        if (objetivoCompletado)
        {
            salidaNivel.SetActive(true);
        }
    }
    public void PasarNivelAnimacion(int indexNivel)
    {
        SceneManager.LoadScene(indexNivel);
    }
}
