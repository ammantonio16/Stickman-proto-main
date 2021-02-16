using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Características movimiento y salto")]
    public float velocity = 2;
    public Joystick joystick;
    public int numJump = 0;
    public int totalJump = 1;
    public bool jump;

    [Header("Turno Player")]
    public ContadordeTiempo ct;
    void Start()
    {
        GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Turn.turnos)
        {

            ct.TiempoRestante();
            jump = (joystick.Vertical > .9f);
            float x = joystick.Horizontal * velocity;
            float y = joystick.Vertical * velocity;
            transform.Translate(x * Time.deltaTime, 0f, 0f);

            if (jump && numJump < totalJump)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
                numJump++;
            }

            if (x < 0)
            {
                GetComponent<Transform>().localScale = new Vector3(-1f, 1f, 1f);
                Turn.direccionbala = false;
            }

            if (x > 0)
            {
                GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                Turn.direccionbala = true;
            }
        }

        if (!Turn.turnos)
        {
            //Debug.Log("esto ahora es falso y no puedo moverme");
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

