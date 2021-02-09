using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWeapon : MonoBehaviour
{
    public GameObject armaContainer;
    public GameObject arrowShot;

    public GameObject buttonContainer;
    public GameObject buttonArrow;

    public void Prueba()
    {
        buttonContainer.SetActive(false);
        buttonArrow.SetActive(true);

        armaContainer.SetActive(true);
        arrowShot.SetActive(false);
    }

    public void Prueba2()
    {
        buttonContainer.SetActive(true);
        buttonArrow.SetActive(false);

        armaContainer.SetActive(false);
        arrowShot.SetActive(true);
    }

}