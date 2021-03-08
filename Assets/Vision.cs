using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public Transform visionEnemy;

    private void Update()
    {
        RaycastparaSaltar();
    }
    void RaycastparaSaltar()
    {
        RaycastHit2D hit = Physics2D.Raycast(visionEnemy.position, -visionEnemy.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(visionEnemy.position, new Vector3(-2f, 0f, 0f));
    }
}
