using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class WeaponController : MonoBehaviour
{
    [Header("Disparo Player")]
    public Transform RaySpawn;
    public float rango;
    public GameObject bullet;
    public GameObject balaClon;
    public int numeroDisparos;
    [SerializeField]
    public int totalDisparos;
    public float cooldown;
    public Collider2D[] ignore;

    [Header("Objetivo de disparo en la escena")]
    public Transform mira;
    public Transform brazo;

    void Update()
    {

        mira.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if (Input.GetButtonDown("Fire1"))
        {
            if(ignore != null)
            {
                foreach (Collider2D c in ignore)
                    {
                        if (c.bounds.Contains(mira.position))
                            return;
                    }
            }
         AtaqueBotton(); 
        }

    }

    private void LateUpdate()
    {
        brazo.up = brazo.position - mira.position;
    }


    public void AtaqueBotton()
    {
        if (numeroDisparos < totalDisparos)
        {
            balaClon = Instantiate(bullet, transform.position, transform.rotation);
            numeroDisparos++;
            Turn.seguirBala = true;
            Turn.moverCamera = false;
        }
    }
}
