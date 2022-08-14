using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensorMinero : MonoBehaviour
{
    Rigidbody2D rb;
    public float velocidad;
    public bool interruptorBajar;
    [SerializeField] Transform[] posiciones;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ascendor();
    }
    void Ascendor()
    {
        //Abajo
        if (interruptorBajar)
        {
            Vector3 movePosition = transform.position;
            movePosition.y = Mathf.MoveTowards(transform.position.y, posiciones[0].position.y, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);
        }
        //Arriba
        else
        {
            Vector3 movePosition = transform.position;
            movePosition.y = Mathf.MoveTowards(transform.position.y, posiciones[1].position.y, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { Debug.Log("Player ha subido al ascensor"); collision.gameObject.transform.parent = transform; };
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { Debug.Log("Player ha subido al ascensor"); collision.gameObject.transform.parent = null; }
    }
}
