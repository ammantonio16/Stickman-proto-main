using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public int index;
    public string nombreScena;
  public void options()
    {
        SceneManager.LoadScene(nombreScena);
    }
}
