using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject follow;
    public Vector2 minCam, maxCam;
    public GameObject enemyFollow;

    
    void Start()
    {
        
    }

    
    void FixedUpdate ()
    {
        if (Turn.turnos)
        {
            float posX = follow.transform.position.x;
            float posY = follow.transform.position.y;

            transform.position = new Vector3
                (Mathf.Clamp(posX, minCam.x, maxCam.x),
                 Mathf.Clamp(posY, minCam.y, maxCam.y),
                 transform.position.z);
        }
        if (!Turn.turnos)
        {
            float posX = enemyFollow.transform.position.x;
            float posY = enemyFollow.transform.position.y;

            transform.position = new Vector3
                (Mathf.Clamp(posX, minCam.x, maxCam.x),
                 Mathf.Clamp(posY, minCam.y, maxCam.y),
                 transform.position.z);
        }
    }
        
}

