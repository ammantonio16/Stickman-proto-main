using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarEscalaConMira : MonoBehaviour
{
    public Transform miraVersion2;
    public Transform centroDelPersonaje;
    public static Vector2 calculo;
    public GameObject player;
    void Start()
    {
        
    }

    void Update()
    {
        
        calculo = new Vector2(miraVersion2.position.x - centroDelPersonaje.position.x, miraVersion2.position.y - centroDelPersonaje.position.y);
        if(calculo.x < 0)
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (calculo.x > 0)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
