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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            if (enemy.GetComponent<ZombieLife>().vidaZombie <= 0)
            {
                enemy = null;
            }
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
                player.GetComponent<JugadorMovimiento>().siendoEmpujado = true;
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
                enemy.GetComponent<ZombieLife>().DañoRecibidoEnemy(10);
                if (enemy.GetComponent<ZombieLife>().vidaZombie > 0)
                {
                    enemy.GetComponent<Enemigo>().enabled = false;
                    Vector2 diferencia2 = enemy.transform.position - transform.position;
                    diferencia2 = diferencia2.normalized * fuerzaEmpuje;
                    enemy.AddForce(diferencia2, ForceMode2D.Impulse);
                    //Quiza desactivar EnemigoIA momentaneamente para que no se recoloque rápidamente si tiene dos sombis
                    StartCoroutine(KnockOut(enemy));
                    //Es posible que esto se adhiera a cambios, ya que aun queda por probar cuando hay 2 sombis y ver como funciona con la velocidad de la IA enemiga. 
                }
                if (enemy.GetComponent<ZombieLife>().vidaZombie <= 0)
                {
                    enemy = null;
                }
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

