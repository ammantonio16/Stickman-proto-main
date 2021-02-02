using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public Image lifeBar;
    public float actualLife;
    public float maxLife;

    public Bullet dañoBala;



    // Update is called once per frame
    void Update()
    {
        lifeBar.fillAmount = actualLife / maxLife;
        
    }

    
    public void VidaBaja (float daño)
    {
        
        actualLife = actualLife - daño;

    }
}
