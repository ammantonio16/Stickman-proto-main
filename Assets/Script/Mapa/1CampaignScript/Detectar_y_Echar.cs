using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Detectar_y_Echar : MonoBehaviour
{
    [Header("Player Data")]
    JugadorMovimiento movPlayer;
    [SerializeField] BloquearMovimientoPlayer player;
    [Header("Enemy Data")]
    public SoldadoNormal soldier;
    [Header("Scene")]
    public int sceneReload;
    public int spawn;

    public bool evento;
    public bool balaInside;

    ObjetoInteractivo objInter;

    public enum SecuenceDetect
    {
        girarSoldado,
        showDialogo,
        resetScene

    }
    public SecuenceDetect detectScene;


    float timeDialogo;
    private void Awake()
    {
        movPlayer = FindObjectOfType<JugadorMovimiento>();
        objInter = GetComponent<ObjetoInteractivo>();
    }

    // Update is called once per frame
    void Update()
    {
        //Change the script: SoldadoDet/Echar out, now you have the same script in all soldier, but with
        //the difference that soldier with trigger will have low range
        //Hacer script aditional con la rama nivel 2 si el estatus es true destruye esto
        if (evento && !soldier.berserker && soldier.GetComponent<SoldierLife>().vida > 0 && !balaInside) 
        {
            Secuence();
        }
        if (soldier.GetComponent<SoldierLife>().vida <= 0 || soldier.berserker) 
        {
            Destroy(this);
            player.HabilitarMovPlayer();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaPlayer")) balaInside = true;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (movPlayer.onGround && collision.gameObject.CompareTag("Player"))
        {
            evento = true;
            if(!balaInside && !ArmaController.preCollisionObject)player.BloquearMovPlayer();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaPlayer")) balaInside = false;
    }

    void Secuence()
    {
        switch (detectScene)
        {
            case SecuenceDetect.girarSoldado:
                //El enemigo se gire
                if (movPlayer.transform.localScale.x == 1) soldier.transform.localScale = new Vector3(-1f, 1f, 1f);
                else if (movPlayer.transform.localScale.x == -1) soldier.transform.localScale = new Vector3(1f, 1f, 1f);
                EstructuraNiveles.nivel = sceneReload;
                if(!balaInside)detectScene = SecuenceDetect.showDialogo;
                break;
            case SecuenceDetect.showDialogo:
                timeDialogo += Time.deltaTime;
                if(timeDialogo >= 1) objInter.DetenerDialogo();
                if (timeDialogo >= 2) detectScene = SecuenceDetect.resetScene;
                break;
            case SecuenceDetect.resetScene:
                AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
                CheckPointController.numeroCheckPoint = spawn;
                break;
        }
    }
}
