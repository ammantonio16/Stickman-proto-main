using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrocutar : MonoBehaviour
{
    GameObject vidaPlayer;
    //GameObject vidaEnemy;
     public float daño = 10f;
    void Start()
    {
        vidaPlayer = GameObject.FindWithTag("Player");
        //vidaEnemy = GameObject.FindWithTag("Enemy");
        
        //vidaEnemy.GetComponent<Life>();
    }
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            vidaPlayer.GetComponent<LifePlayer>().VidaBaja(daño);
            //vidaEnemy.GetComponent<Life>().VidaBaja(daño);
        }
    }
}
