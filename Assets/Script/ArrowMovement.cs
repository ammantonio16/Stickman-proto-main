using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public GameObject arrow;
    public float speed;
    public float range;
    public Transform arrowPosition;
    Rigidbody2D rb;

    Vector2 limitRange;
    bool drag = true;
    Vector3 dis;
    

    
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    
    void OnMouseDrag()
    {
        if (!drag)
            return;
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dis = pos - arrowPosition.position;
        dis.z = 0;
        
        if (dis.magnitude > range)
        {
            dis.z = 0;
            dis = dis.normalized * range;
        }
        transform.position = dis + arrowPosition.position;   
    }
    void OnMouseUp()
    {
        if (!drag)
            return;
        drag = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = -dis.normalized * speed * dis.magnitude * speed / range;
    }
}
