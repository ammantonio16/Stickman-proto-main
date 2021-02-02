using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBala : MonoBehaviour
{
    // Start is called before the first frame update


    public WeaponController clon;
    public Vector2 minCam, maxCam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     float posXbala = clon.balaClon.transform.position.x;
     float posYbala = clon.balaClon.transform.position.y;

     transform.position = new Vector3
     (Mathf.Clamp(posXbala, minCam.x, maxCam.x),
      Mathf.Clamp(posYbala, minCam.y, maxCam.y),
      transform.position.z);
    }
}
