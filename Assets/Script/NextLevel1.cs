using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel1 : MonoBehaviour
{
    public Collider2D sigNivel;

    public void OnTriggerEnter2D(Collider2D colli)
    {
        if (colli.gameObject.tag == ("Player"))
        {
            SceneManager.LoadScene("Campaign3");
        }
    }
}
