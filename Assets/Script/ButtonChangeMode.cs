using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class ButtonChangeMode : MonoBehaviour
{
    public MoveWeapon changeMode;
    public GameObject fireButton;
    public GameObject moveButton;


    public GameObject startPosition;

    public void Attack()
    {
        changeMode.enabled = true;
        fireButton.SetActive(false);
        moveButton.SetActive(true);
    }
    public void Move()
    {
        changeMode.enabled = false;
        fireButton.SetActive(true);
        moveButton.SetActive(false);

        startPosition.transform.rotation = Quaternion.identity;
        
    }
}
