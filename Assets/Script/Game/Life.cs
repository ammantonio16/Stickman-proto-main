using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Life : MonoBehaviour
{
    //public Image lifeBar;
    public float actualLife;
    public float maxLife;

    private void Start()
    {
       
    }
    void Update()
    {
        VidaBaja(maxLife);
    }

    public void VidaBaja (float daño)
    {
        
        actualLife = actualLife - daño;
        if (actualLife <= 0)
        {
            Debug.Log("Estoy Muerto");
            Destroy(this.gameObject);
        }

    }
}
