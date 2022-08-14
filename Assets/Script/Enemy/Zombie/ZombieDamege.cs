using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamege : MonoBehaviour
{
    public float fuerzaEmpuje;
    public Transform centerSombi;
    float timeShoot;
    Rigidbody2D player;
    public Rigidbody2D enemy;

    public float knockTime;

    GameObject prayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponentInParent<Rigidbody2D>();
            if(player != null)
            {
                Debug.Log("guacamole;");
                player.GetComponent<JugadorMovimiento>().velocidad = 0;
                //player.GetComponent<JugadorMovimiento>().siendoEmpujado = true;
                player.GetComponent<LifePlayer>().VidaBaja(10);
                Vector2 diferencia = player.transform.position - transform.position;
                diferencia = diferencia.normalized * fuerzaEmpuje;
                player.AddForce(diferencia, ForceMode2D.Impulse);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = collision.GetComponentInParent<Rigidbody2D>();
            if (enemy != null)
            {
                Debug.Log("guacamole;");
                
                
            }
        }
        IEnumerator KnockOut(Rigidbody2D enemy)
        {
            if(enemy != null)
            {
                yield return new WaitForSeconds(knockTime);
                enemy.velocity = Vector2.zero;
                enemy.GetComponent<Enemigo>().enabled = true;
                //Activar enemigoIA

            }
        }
        
    }
}

