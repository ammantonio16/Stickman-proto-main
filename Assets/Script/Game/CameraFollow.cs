using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Player")]
    public GameObject follow;
    //public WeaponController clon;

    [Header("Rango Camara")]
    public Vector2 izqCam;
    public Vector2 dchaCam;

    //[Header("Enemy")]
    //public GameObject enemyFollow;

    void FixedUpdate ()
    {
        float posX = follow.transform.position.x;
        float posY = follow.transform.position.y;

        transform.position = new Vector3
            (Mathf.Clamp(posX, izqCam.x, dchaCam.x),
             Mathf.Clamp(posY, izqCam.y, dchaCam.y),
             transform.position.z);
        /*if (Turn.moverCamera)
        {
            float posX = follow.transform.position.x;
            float posY = follow.transform.position.y;

            transform.position = new Vector3
                (Mathf.Clamp(posX, izqCam.x, dchaCam.x),
                 Mathf.Clamp(posY, izqCam.y, dchaCam.y),
                 transform.position.z);
        }
        if (Turn.turnos && !Turn.moverCamera)
        {
            if (clon.balaClon == null)
            {
                float posXplayerbala = follow.transform.position.x;
                float posYplayerbala = follow.transform.position.y;

                transform.position = new Vector3
                (Mathf.Clamp(posXplayerbala, minCam.x, maxCam.x),
                 Mathf.Clamp(posYplayerbala, minCam.y, maxCam.y),
                 transform.position.z);
            }
            if (clon.balaClon != null)
            {
                float posXbala = clon.balaClon.transform.position.x;
                float posYbala = clon.balaClon.transform.position.y;

                transform.position = new Vector3
                (Mathf.Clamp(posXbala, minCam.x, maxCam.x),
                 Mathf.Clamp(posYbala, minCam.y, maxCam.y),
                 transform.position.z);
            }
        }
        if (!Turn.turnos)
        {
            float posX = enemyFollow.transform.position.x;
            float posY = enemyFollow.transform.position.y;

            transform.position = new Vector3
                (Mathf.Clamp(posX, minCam.x, maxCam.x),
                 Mathf.Clamp(posY, minCam.y, maxCam.y),
                 transform.position.z);
        }*/
    }
        
}

