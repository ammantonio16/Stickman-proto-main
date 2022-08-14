using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopaCambiar : MonoBehaviour
{
    [Header("Skin Player")]
    public int indexSkin;
    [SerializeField] Skin skin;
    [SerializeField] Animator playerAnim;
    [Header("Soldiers")]
    [SerializeField] Transform soldierPlayerClothe;
    [SerializeField] GameObject soldierNormal;
    [Header("Icons")]
    [SerializeField] SpriteRenderer iconosZone;
    [SerializeField] Sprite icono;
    [Header("Bloquear Player")]
    [SerializeField] BloquearMovimientoPlayer blockPlayer;

    public bool cambiarRopa;
    // Update is called once per frame
    void Update()
    {
        CanPutClothe();
    }
    void CanPutClothe()
    {
        if (cambiarRopa)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                blockPlayer.BloquearMovPlayer();
                AnimationHud.detectar_echar.SetBool("Transicion", true);
                StartCoroutine("CambiarRopa");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            cambiarRopa = true;
            Debug.Log("NArciso");
            SeeIcon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            cambiarRopa = false;
            iconosZone.gameObject.SetActive(false);
        } 
    }
    void SeeIcon()
    {
        iconosZone.gameObject.SetActive(true);
        iconosZone.sprite = icono;
    }
    void ChangeRopa()
    {
        playerAnim.runtimeAnimatorController = skin.skins[indexSkin];
    }
    IEnumerator CambiarRopa()
    {
        yield return new WaitForSeconds(1f);
        //Change Clothes
        ChangeRopa();
        //Teleport SoldierHurt to SoldierNormal
        soldierPlayerClothe.position = soldierNormal.transform.position;
        yield return new WaitForSeconds(.5f);
        //Disable the SoldierNormal
        soldierNormal.SetActive(false);
        //You can see the Scene
        AnimationHud.detectar_echar.SetBool("Transicion", false);
        //You Can Move
        blockPlayer.HabilitarMovPlayer();
        //Stop
        StopCoroutine("CambiarRopa");
    }
}
