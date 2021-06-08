using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class ButtonChangeMode : MonoBehaviour
{
    public WeaponController changeMode;
    public GameObject panelAttack;
    public GameObject panelMove;


    public GameObject startPosition;

    public void Start()
    {

    }



    public void Attack()
    {
        changeMode.enabled = true;
        panelAttack.SetActive(true);
        panelMove.SetActive(false);

    }
    public void Move()
    {
        changeMode.enabled = false;
        panelMove.SetActive(true);
        panelAttack.SetActive(false);


        startPosition.transform.rotation = Quaternion.identity;
        
    }
}
