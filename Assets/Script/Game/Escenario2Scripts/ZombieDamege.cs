using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamege : MonoBehaviour
{
    public float fuerzaEmpuje;
    public GameObject bulletInvisible;
    public Transform centerSombi;
    float timeShoot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player ha entrado");
        if(collision.gameObject.tag == "Player" && Time.time > timeShoot + 0.25)
        {
            Debug.Log("Te he atacado");
            collision.gameObject.GetComponentInParent<Life2Enemy>().actualLife -= 4;
            timeShoot = Time.time;
            GameObject objectKnockback = Instantiate(bulletInvisible, centerSombi.position, centerSombi.rotation );
        }
    }
    //collision.gameObject.GetComponentInParent<Life2Enemy>().actualLife -= 2;
    //collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce(transform.up * fuerzaEmpuje, ForceMode2D.Impulse);
}

