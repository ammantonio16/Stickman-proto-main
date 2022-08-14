using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMask : MonoBehaviour
{
    [SerializeField] PutGasMask mineroCasco;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mineroCasco.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mineroCasco.enabled = true;
        }
    }
}
