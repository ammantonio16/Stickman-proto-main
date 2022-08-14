using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCinematic : MonoBehaviour
{
    PlayerHere player;
    [SerializeField] EscaleraMano playerHiddenComp;

    public int sceneCatching;
    bool enabledEscaleraMano;
    float timeEnabled;
    private void Awake()
    {
        player = GetComponent<PlayerHere>();
    }
    void Update()
    {
        BeginScene();
    }
    void BeginScene()
    {
        if (player.playerImHere)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                enabledEscaleraMano = true;
                AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
                EstructuraNiveles.nivel = sceneCatching;
            }
        }
        if (enabledEscaleraMano)
        {
            timeEnabled += Time.deltaTime;
            if(timeEnabled >= 0.2)
            {
                playerHiddenComp.enabled = false;
            }
        }
    }
}
