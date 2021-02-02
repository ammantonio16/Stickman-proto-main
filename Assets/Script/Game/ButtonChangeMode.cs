using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class ButtonChangeMode : MonoBehaviour
{
    public WeaponController changeMode;
    public GameObject fireButton;
    public GameObject moveButton;


    public GameObject startPosition;

    public CameraFollow desactivarCamera;
    public CameraFollowBala cameraBala;

    public void Attack()
    {
        changeMode.enabled = true;
        fireButton.SetActive(false);
        moveButton.SetActive(true);
        desactivarCamera.enabled = false;
        cameraBala.enabled = true;
    }
    public void Move()
    {
        changeMode.enabled = false;
        fireButton.SetActive(true);
        moveButton.SetActive(false);
        desactivarCamera.enabled = true;
        cameraBala.enabled = false;

        startPosition.transform.rotation = Quaternion.identity;
        
    }
}
