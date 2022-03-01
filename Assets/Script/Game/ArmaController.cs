using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmaController : MonoBehaviour
{
    public Transform raySpawn;
    public GameObject bullet;
    public Transform mira;
    public Transform brazo;
    public static bool activarDisparo;
    public Transform playerScale;
    public int municionTotal;
    public Text municionText;
    public CanvasGroup sinMunicionText;
    void Start()
    {
        activarDisparo = false;
    }

    // Update is called once per frame
    void Update()
    {
        municionText.text = municionTotal.ToString();
        sinMunicionText.alpha -= 0.005f;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activarDisparo = !activarDisparo;
        }
        if (activarDisparo)
        {
            mira.SetActive(true);
            mira.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            if(municionTotal > 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Ataque();
                    municionTotal -= 1;
                }
            }
            else if(municionTotal <= 0)
            {
                municionText.color = new Color(255, 0, 0);
                if (Input.GetButtonDown("Fire1"))
                {
                    sinMunicionText.alpha = 1;
                    Debug.Log("Estas sin munición");
                }

            }
        }
        else
        {
            mira.localPosition = new Vector2(0f, mira.localPosition.y);
            mira.SetActive(false);
            brazo.transform.rotation = Quaternion.identity;
        }
    }
    private void LateUpdate()
    {
        if (activarDisparo)
        {
            brazo.up = brazo.position - mira.position;
        }
        
        //brazo.up = brazo.position - mira.position;
    }
    void Ataque()
    {
        GameObject balaClon = Instantiate(bullet, raySpawn.position, raySpawn.rotation);
    }
}
