using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera CM")]
    public GameObject cmVCam1;
    //public WeaponController clon;




    //posterior mente Limitar los lados

    void FixedUpdate ()
    {

        transform.position = new Vector3(cmVCam1.transform.position.x, cmVCam1.transform.position.y, transform.position.z);

    }
        
}

