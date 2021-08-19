using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmaCoche : MonoBehaviour
{
    public GameObject audioAlarm;
    public GameObject spheraZombieFollow;
    Transform capo;
    void Start()
    {
        capo = GetComponent<Transform>();
        //spheraZombieFollow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BalaPlayer")
        {
            AtraerAlarmZombie.alarmaEncendida = true;
            Destroy(collision.gameObject);
            Debug.Log("El coche ha recibido un disparo");
            Instantiate(audioAlarm,capo);
            spheraZombieFollow.SetActive(true);
            //GameObject atraer = Instantiate(spheraZombieFollow, capo);

        }
    }
}
