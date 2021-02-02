using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHit : MonoBehaviour
{
    /*public GameObject LifeBar;

    // Start is called before the first frame update
    void Start()
    {
        Image LifeBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        LifeBar.GetComponent<Image>().fillAmount = LifeBar.GetComponent<Image>().fillAmount - 0.1f;
    }*/

    public float vida = 100f;

    public Image barraDeVida;

    void Update()
    {
        barraDeVida.fillAmount = vida / 100f;
        
    }
}
