using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveClothe : MonoBehaviour
{
    [SerializeField] RopaCambiar ropaCambiar;
    SoldierLife vidaSoldier;
    private void Start()
    {
        vidaSoldier = GetComponent<SoldierLife>();
    }
    void Update()
    {
        if(vidaSoldier.vida <= 0)
        {
            ropaCambiar.gameObject.SetActive(true);
        }
    }
}
