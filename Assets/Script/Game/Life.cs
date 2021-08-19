using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Life : MonoBehaviour
{
    public Image lifeBar;
    public float actualLife;
    //public float maxLife;
    public Animator anim;
    

    
    void Update()
    {
        //VidaBaja(maxLife);

        //lifeBar.fillAmount = actualLife /*/ maxLife*/;
    }

    public void VidaBaja (float daño)
    {
        
        actualLife = actualLife - daño;
        if (actualLife <= 0)
        {
            //Destroy(this.gameObject);
            anim.SetBool("Dead", true);
            
        }
    }
}
