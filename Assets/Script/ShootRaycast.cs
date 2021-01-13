using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRaycast : MonoBehaviour
{
    
    public Transform RaySpawn;
    public float rango;
    // Start is called before the first frame update
    void Start()
    {

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
        Gizmos.color = Color.red;
        Gizmos.DrawRay(RaySpawn.position, RaySpawn.right);
    }
}
