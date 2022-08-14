using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carretilla : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] SoldierLife soldadoVida;

    int vidaActual;
    bool screenshot;
    private void Awake()
    {
        soldadoVida = GetComponentInParent<SoldierLife>();
    }
    void Start()
    {
        screenshot = true;
    }

    // Update is called once per frame
    void Update()
    {
        AnularParent();
        VidaActual();
    }
    void AnularParent()
    {
        if (soldadoVida.vida != vidaActual|| soldadoVida.GetComponent<SoldadoNormal>().berserker || soldadoVida.GetComponent<SoldadoNormal>().seeCadaverWarning) { Debug.Log("Ya no hijo"); transform.parent = null; }
    }
    void VidaActual()
    {
        if (screenshot)
        {
            vidaActual = soldadoVida.vida;
            screenshot = false;
        }
    }
}
