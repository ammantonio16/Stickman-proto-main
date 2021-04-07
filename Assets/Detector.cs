using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameObject detector;
    public GameObject enemy;

    public void Start()
    {
        detector.GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Instantiate(enemy, new Vector3(9.17f, -6.74f, 0f), Quaternion.identity);
        }
    }
}
