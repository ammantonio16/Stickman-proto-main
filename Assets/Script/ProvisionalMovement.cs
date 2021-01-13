using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvisionalMovement : MonoBehaviour
{
    public float velocity = 2;
    SpriteRenderer spinX;
    void Start()
    {
        GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        transform.Translate(x * Time.deltaTime * velocity, 0f, 0f);
        if (x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if(x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
