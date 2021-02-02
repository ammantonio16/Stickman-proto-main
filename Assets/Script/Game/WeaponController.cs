using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class WeaponController : MonoBehaviour
{
    public Transform spinEmpty;
    public Transform RaySpawn;
    public float rango;
    public GameObject bullet;
    Vector3 diferencial;

    public Transform mira;
    public Transform container;
    public Transform mano;

    private int numeroDisparos = 0;
    private int totalDisparos = 10;

    public ContadordeTiempo textoReloj;

    public GameObject balaClon;

    public Enemy enemy;

    void Start()
    {

    }

    void Update()
    { 
        
        mira.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        /*mano.position = mira.position;*/
        if (numeroDisparos <= totalDisparos)
        {
            if (Input.GetButtonDown("Fire1")) 
            {
                AtaqueBotton();
                
            }
            


        }
    }
    private void LateUpdate()
    {
        container.up = container.position - mira.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(RaySpawn.position, (mira.position - container.position));
    }
    public void AtaqueBotton()
    {
        balaClon = Instantiate(bullet, transform.position, transform.rotation);
        Turn.seguirBala = true;
        numeroDisparos++;
        RaycastHit2D hit = Physics2D.Raycast(RaySpawn.position, (mira.position - container.position).normalized, 1000f, ~(1 << 10));
        
    }
    //RaycastShot();
    /*void RaycastShot()
    {
        RaycastHit2D hit = Physics2D.Raycast(RaySpawn.position, RaySpawn.right);
        if (hit)
        {
            Debug.Log(hit.collider.name);
        }
        float z = Mathf.Atan2(mira.rotation.y, mira.rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }*/
}
