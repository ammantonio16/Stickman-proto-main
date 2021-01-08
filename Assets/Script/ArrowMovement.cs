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
    bool drag = true;
    void Start()
    {
        arrow.AddComponent<Rigidbody2D>();
        arrow.AddComponent<BoxCollider2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    void Update()
    {
        
    }
    void OnMouseDrag()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dis = new Vector2 (pos.x - arrowPosition.position.x, pos.y - arrowPosition.position.y);
        
        if (dis.magnitude < range)
        {
            dis = dis.normalized * range;
        }
        transform.position = new Vector2 (dis.x + arrowPosition.position.x, dis.y + arrowPosition.position.y);
        
    }
    void OnMouseUp()
    {
        
    }
}
