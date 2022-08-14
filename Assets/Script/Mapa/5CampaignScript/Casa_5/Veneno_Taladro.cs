using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veneno_Taladro : MonoBehaviour
{
    [SerializeField] VidaPlayer playerLife;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Perico");
            playerLife.Daño(1f);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<SoldierLife>().Daño(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) Debug.Log("Perico");
    }
}
