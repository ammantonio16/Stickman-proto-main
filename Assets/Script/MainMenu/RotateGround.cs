using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGround : MonoBehaviour
{
    public GameObject Ground;
    public float xAngle, yAngle, zAngle;


    void Awake()
    {
        //Ground.transform.position = new Vector3(0.0f, -4.0f, 0.0f);
        Ground.transform.Rotate(0.0f, 5.0f * Time.deltaTime, 0.0f, Space.World);
    }

    void Update()
    {
        Ground.transform.Rotate(xAngle, yAngle, zAngle, Space.Self );
    }
}
