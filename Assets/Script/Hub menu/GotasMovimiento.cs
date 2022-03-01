using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotasMovimiento : MonoBehaviour
{
    public float velocidad;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + velocidad, transform.position.y + velocidad);
        Destroy(this.gameObject, 3f);
    }
}
