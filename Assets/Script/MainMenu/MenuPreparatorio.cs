using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPreparatorio : MonoBehaviour
{
    public void PreparatorioScene()
    {
        SceneManager.LoadScene("MenuPreparatorio");
        Time.timeScale = 1f;
    }
}
