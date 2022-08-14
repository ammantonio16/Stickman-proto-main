using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BloquearMovimientoPlayer : MonoBehaviour
{
    public JugadorMovimiento player;
    public VidaPlayer vidaPlayer;
    public ArmaController pistol;
    public B8Arma b8;
    public ChangeWeapon conditionToChangeWeapon;

    public void BloquearMovPlayer()
    {
        //Deactive LifePlayer; Note: Keep the live when bloq is active
        vidaPlayer.enabled = false;
        //Deactive Player
        player.enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        player.GetComponent<Animator>().SetBool("Walk", false);
        //Deactivate Pistol
        pistol.activarDisparo = false;
        pistol.enabled = false;
        pistol.mira.gameObject.SetActive(false);
        //Deactivate B8
        b8.activarB8 = false;
        b8.enabled = false;
        b8.mira.gameObject.SetActive(false);
        //Deactivate ChangeWeapon
        conditionToChangeWeapon.enabled = false;
    }
    public void HabilitarMovPlayer()
    {
        vidaPlayer.enabled = true;
        player.enabled = true;
        pistol.enabled = true;
        pistol.mira.gameObject.SetActive(true);
        b8.mira.gameObject.SetActive(true);
        b8.enabled = false;
        conditionToChangeWeapon.enabled = false;
    }
}
