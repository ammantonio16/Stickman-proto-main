using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B8Arma : MonoBehaviour
{

    [Header("Info B8")]
    public Transform spawnB8;
    public GameObject B8;
    public Transform mira;
    public Transform brazo;
    public SpriteRenderer b8Sprite;
    [Header("Municion Text")]
    public int municionTotal = 1;
    [Header("Pistol")]
    [SerializeField] ArmaController pistol;
    [Header("OcultacionByWeapon")]
    [SerializeField] OcultacionByWeapon ocultacion;
    public bool activarB8;

    Camera pruebaCamera;

    //B8 Weapon have only 1 ammo so you don't need municionText and when you throw B8 disappear from your hand and inactive the mira
    private void Awake()
    {
        pruebaCamera = GameObject.FindGameObjectWithTag("CameraDisparo").GetComponent<Camera>();
        //municionTotal = 1;
    }
    void Start()
    {
        b8Sprite = GetComponent<SpriteRenderer>();
        //activarB8 = false;
        municionTotal = 1;
    }
    void Update()
    {
        DrawB8();
        UseB8();
    }
    private void LateUpdate()
    {
        if (activarB8)
        {
            brazo.up = brazo.position - mira.position;
        }

    }
    void DrawB8()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(municionTotal > 0) activarB8 = !activarB8;

        }
    }
    void UseB8()
    {
        if (activarB8)
        {
            b8Sprite.enabled = true;
            //Mira sprite, no objeto
            mira.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            mira.position = pruebaCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -pruebaCamera.transform.position.z));
            if(municionTotal > 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Ataque();
                    ocultacion.visible = true;
                    activarB8 = false;
                    pistol.activarDisparo = false;
                    municionTotal -= 1;
                }
            }
        }
        else
        {
            Debug.Log("No B8");
            if(municionTotal <= 0) b8Sprite.enabled = false;
            mira.localPosition = new Vector2(0f, mira.localPosition.y);
            mira.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            brazo.transform.rotation = Quaternion.identity;
        }
    }
    void Ataque()
    {
        GameObject balaClon = Instantiate(B8, spawnB8.position, spawnB8.rotation);
    }
}
