using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarPuertas : MonoBehaviour
{
    SpriteRenderer verde;
    public Electrocutar agua1;
    public Electrocutar agua2;
    private void Start()
    {
        verde = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ArmaRedonda")
        {
            verde.color = new Color(0f, 255f, 0f, 255f);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Electricidad");
            foreach (GameObject target in gameObjects)
            {
                GameObject.Destroy(target);
            }
            agua1.enabled = false;
            agua2.enabled = false;
        }
    }
}

