using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CientificoCinematic : MonoBehaviour
{
    public int indexMod;
    [Header("Player Data Scene")]
    [SerializeField]BloquearMovimientoPlayer playerMov;
    [SerializeField]Transform player;
    [Header("Scientific Data Scene")]
    [SerializeField] SoldadoNormal scientific;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCamera;
    [Header("Actions in Scene")]
    [SerializeField]Polipasto_Normal plataforma_1;
    public GameObject blackScreen;
    enum Scientific_Scenes
    {
        Start_Scene,
        Middle_Scene_1,
        Middle_Scene_2,
        End_Scene,
    }

    Scientific_Scenes scientific_Cinematic;

    bool beginCinematic;
    float timeNextScene;

    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) FinishScientificCinematic();
    }

    // Update is called once per frame
    private void Update()
    {
        ScientificScript();
        //The first and the last scenes are the only that made the same always, theorycally 
    }
    void ScientificScript()//GAmeobject as an objective to Camera
    {
        if (beginCinematic && playerMov.player.onGround)
        {
            switch (scientific_Cinematic)
            {
                //Begin Cinematic: inmobilize the Player, transition and the last change camera 
                case Scientific_Scenes.Start_Scene:
                    playerMov.BloquearMovPlayer();
                    AnimationHud.detectar_echar.SetBool("Transicion", true);
                    if(timeNextScene >= 2)
                    {
                        mainCamera.Follow = scientific.transform;
                        scientific_Cinematic = Scientific_Scenes.Middle_Scene_1;
                        
                    }
                    else timeNextScene += Time.deltaTime;
                    break;
                //And action to continue the Scene (this is when event suscribe, like events)
                case Scientific_Scenes.Middle_Scene_1:
                    Debug.Log("Second Stage");
                    plataforma_1.enabled = true;
                    AnimationHud.detectar_echar.SetBool("Transicion", false);
                    if(timeNextScene <= 0)
                    {
                        scientific_Cinematic = Scientific_Scenes.Middle_Scene_2;
                        
                    }
                    else timeNextScene -= Time.deltaTime;
                    break;
                //Continue the action
                case Scientific_Scenes.Middle_Scene_2:
                    Debug.Log("Third Stage");
                    if (timeNextScene >= 2)
                    {
                        scientific.enabled = true;
                        if (scientific.transform.position.x == scientific.ubicacionesDirigir[0].position.x)
                        {
                            blackScreen.SetActive(true);
                            //Add Sound of door close with lock
                            AnimationHud.detectar_echar.SetBool("Transicion", true);
                            scientific_Cinematic = Scientific_Scenes.End_Scene;
                            
                        }
                    }
                    else timeNextScene += Time.deltaTime;
                    break;
                //Transition is true, you can move; the GameObjectVariable is true and Desactive the Cinematic Object
                case Scientific_Scenes.End_Scene:
                    Debug.Log("Fourth Stage");
                    mainCamera.Follow = player;
                    if(timeNextScene <= 0)
                    {
                        blackScreen.SetActive(false);
                        playerMov.HabilitarMovPlayer();
                        AnimationHud.detectar_echar.SetBool("Transicion", false);
                        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
                        scientific.gameObject.SetActive(false);
                        this.gameObject.SetActive(false);
                        
                    }
                    else timeNextScene -= Time.deltaTime;
                    break;
            }
        }
    }
    void FinishScientificCinematic()
    {
        plataforma_1.enabled = true;
        scientific.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) beginCinematic = true;
    }
}
