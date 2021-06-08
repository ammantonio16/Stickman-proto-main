using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life2Enemy : MonoBehaviour
{
    public float actualLife;
    public float maxLife;


    void Update()
    {
        VidaBaja(maxLife);
    }

    public void VidaBaja(float daño)
    {

        actualLife = actualLife - daño;
        if (actualLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
