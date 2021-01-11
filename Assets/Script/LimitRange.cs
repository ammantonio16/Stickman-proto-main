using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRange : MonoBehaviour
{
    private Transform Limite;
    public Vector2 rangoH = Vector2.zero;
    public Vector2 rangoV = Vector2.zero;
    private void LateUpdate()
    {
        Limite.position = new Vector3(
            Mathf.Clamp(transform.position.x, rangoV.x, rangoV.y),
            Mathf.Clamp(transform.position.y, rangoH.x, rangoH.y),
            transform.position.z);
    }
    void Start()
    {
        Limite = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
