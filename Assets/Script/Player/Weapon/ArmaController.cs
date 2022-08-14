using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmaController : MonoBehaviour
{
    [Header("Info Weapon")]
    public Transform raySpawn;
    public GameObject bala;
    public Transform mira;
    public Transform brazo;
    public bool activarDisparo;
    [Header("Raycast Pistol")]
    public static bool preCollisionObject;
    public LayerMask maskCollision;
    [Header("Aim")]
    public float timeAppearAim;
    public float timeAppearAimLimit;
    [Header("Municion Text")]
    public int municionTotal;
    public Text municionText;
    [Header("Disparos")]
    public EffectDisparo shootAnim;
    float cooldown;
    public float refreshWeapon = 0.5f;
    SoundManager soundManager;

    [Header("Weapon Sprite")]
    public SpriteRenderer weapon;
    public Sprite pistolaSprite;
    public GameObject bullet;
    //Cambiar Layer
    [Header("Infiltración Data")]
    [SerializeField] OcultacionByWeapon theySeeYou;



    Camera pruebaCamera;
    private void Awake()
    {
        pruebaCamera = GameObject.FindGameObjectWithTag("CameraDisparo").GetComponent<Camera>();
    }
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        municionText.text = municionTotal.ToString();

        DrawWeapon();
        UseWeapon();
    }
    private void LateUpdate()
    {
        if (activarDisparo)
        {
            brazo.up = brazo.position - mira.position;
        }
        //mira.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        //brazo.up = brazo.position - mira.position;
    }

    void DrawWeapon()
    {
        //Draw the weapon or keep it
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activarDisparo = !activarDisparo;
        }
    }
    void UseWeapon()
    {

        CoolDownWeapon();
        //Draw You Weapon
        if (activarDisparo)
        {
            {
                //They can see you
                theySeeYou.VisibleOn();
                //You aim with the gun
                mira.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                municionText.enabled = true;
                mira.position = pruebaCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -pruebaCamera.transform.position.z));
                
                if (municionTotal > 0)
                {
                    //Añadir cooldown;
                    if (cooldown <= 0)
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            //Sound
                            soundManager.SeleccionAudio(soundManager.audiosArmas, 0, 1f, 0);
                            //Effect
                            shootAnim.AnimationEffectDisparo();
                            //Attack
                            Ataque();
                            cooldown = refreshWeapon;
                            municionTotal -= 1;
                            theySeeYou.visible = true;
                        }
                    }
                }
                else if (municionTotal <= 0)
                {
                    //Show your ammo in red 
                    municionText.color = Color.red;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        soundManager.SeleccionAudio(soundManager.audiosArmas, 1, 1f, 0);
                        Debug.Log("Estas sin munición");
                    }

                }
            }
        }
        //Keep your Weapon
        else
        {
            municionText.enabled = false;
            mira.localPosition = new Vector2(0f, mira.localPosition.y);
            mira.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            brazo.transform.rotation = Quaternion.identity;
            theySeeYou.VisibleOff();
            if (!ListaAttackSoldado.modeSoldiers.activeAllAttackSoldiers) theySeeYou.isVisible.enabled = false;
        }
    }
    void CoolDownWeapon()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (cooldown < 0)
        {
            cooldown = 0;
        }
    }
    void Ataque()
    {
        GameObject balaClon = Instantiate(bullet, raySpawn.position, raySpawn.rotation);
        DrawRay();
    }
    void DrawRay()
    {
        RaycastHit2D rayPistol;
        rayPistol = Physics2D.Raycast(raySpawn.position, DistanceMiraPistol().normalized, 10 ,maskCollision);
        if (rayPistol)
        {
            preCollisionObject = true;
            Debug.Log("Gorgoncela");
        }
    }
    Vector2 DistanceMiraPistol()
    {
        Vector2 distanceMiraPistol;
        return distanceMiraPistol = mira.position - raySpawn.position;
    }
}

