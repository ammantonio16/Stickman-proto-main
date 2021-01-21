using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRaycast : MonoBehaviour
{
    
    public Transform RaySpawn;
    public float rango;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastShot();
    }
    void RaycastShot()
    {
        RaycastHit2D hit = Physics2D.Raycast(RaySpawn.position, RaySpawn.right);
        if (hit)
        {
            Debug.Log(hit.collider.name);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(RaySpawn.position, RaySpawn.right * rango);
    }

}
