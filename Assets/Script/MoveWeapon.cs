using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class MoveWeapon : MonoBehaviour
{
    public Transform spinEmpty;
    public Transform RaySpawn;
    public float rango;
    public GameObject bullet;
    Vector3 diferencial;

    public Transform mira;
    public Transform container;
    public Transform mano;

    void Start()
    {
    }

    void Update()
    { 
        
        mira.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        /*mano.position = mira.position;*/
        AtaqueBotton();
    }
    private void LateUpdate()
    {
        container.up = container.position - mira.position;
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
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(RaySpawn.position, RaySpawn.right * rango);
    }*/
    public void AtaqueBotton()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, RaySpawn.position, RaySpawn.rotation);
        }
        
    }
}
