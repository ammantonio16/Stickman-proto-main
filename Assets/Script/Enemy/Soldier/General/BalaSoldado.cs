using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSoldado : MonoBehaviour
{
    float vidaBala;
    float vidaBalaMax;
    private void Start()
    {
        vidaBalaMax = 5f;
    }
    private void Update()
    {
        vidaBala += Time.deltaTime;
        if (vidaBala >= vidaBalaMax) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {Debug.Log("Player golpeado"); Destroy(this.gameObject); }
        if (collision.gameObject.CompareTag("Ground")) Destroy(this.gameObject);
    }
}
