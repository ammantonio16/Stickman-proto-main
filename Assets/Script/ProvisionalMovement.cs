using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvisionalMovement : MonoBehaviour
{
    public float velocity = 2;
    public Joystick joystick;
    public int numJump = 0;
    public int totalJump = 1;
    public bool jump;

    

    void Start()
    {
        GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = (joystick.Vertical > .9f);
        float x = joystick.Horizontal * velocity;
        float y = joystick.Vertical * velocity;
        transform.Translate(x * Time.deltaTime, 0f, 0f);
        if (jump && numJump < totalJump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f),ForceMode2D.Impulse);
            numJump++;
        }









        if (x < 0)
        {
            GetComponent<Transform>().localScale = new Vector3(-1f, 1f, 1f);

        }
        if (x > 0)
        {
            GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            numJump = 0;
        }
    }
}

