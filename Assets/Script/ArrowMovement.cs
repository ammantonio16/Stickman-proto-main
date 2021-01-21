using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float speed;
    public float range;
    public Transform arrowPosition;
    Rigidbody2D rb;

    public GameObject arrow;
    public Transform spawn;
    GameObject arrowClon;


    bool drag = true;
    Vector3 dis;
    SpriteRenderer sprite;

    
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        sprite = GetComponent<SpriteRenderer>();
        /*sprite.enabled = false;*/
    }
    void OnMouseDrag()
    {
        /*sprite.enabled = false;*/
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
        /*sprite.enabled = true; */
        if (!drag)
            return;
        drag = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = -dis.normalized * speed * dis.magnitude * speed / range;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(arrow, spawn.position, spawn.rotation);
            Destroy(this.gameObject, 2f);
        }
    }
}
