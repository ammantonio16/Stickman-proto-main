using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryRigidbody : MonoBehaviour
{
    public bool triggerAsSensor = false;
    public List<Rigidbody2D> rbList = new List<Rigidbody2D>();

    public Vector3 lastPosition;
    Transform myTransform;

    [HideInInspector] public Rigidbody2D rb;
    void Start()
    {
        myTransform = transform;
        lastPosition = myTransform.position;
        rb = GetComponent<Rigidbody2D>();

        
    }
    private void FixedUpdate()
    {
        //Mantenerte en el ascensor
        if(rbList.Count > 0)
        {
            for (int i = 0; i < rbList.Count; i++)
            {
                Rigidbody2D rb = rbList[i];
                Vector3 velocity = (myTransform.position - lastPosition);
                rb.transform.Translate(velocity,myTransform);
            }
        }

        lastPosition = myTransform.position;
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Rigidbody2D rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
        if(rb2D != null)
        {
            rbList.Add(rb2D);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rbList.Remove(rb2D);
        }
    }

    public void Add(Rigidbody2D rb)
    {
        //Si no esta en la lista añadelo
        if (!rbList.Contains(rb)) Add(rb);
    }
    public void Remove(Rigidbody2D rb)
    {
        //Si esta en la lista quitalo
        if (rbList.Contains(rb)) Add(rb);
    }
}
