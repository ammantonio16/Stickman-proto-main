using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginCinematic_1 : MonoBehaviour
{
    public float timeToStartCinematic;
    public int indexModi;

    [Header("Box with Player Data")]
    [SerializeField] GameObject boxWithPlayer;
    [SerializeField] Transform UbiBoxPlatform;

    [Header("Camera Data")]
    [SerializeField] Transform cameraFollowBox;
    [SerializeField] Transform ubiDirectCamera;

    [Header("Polipasto")]
    [SerializeField]Polipasto_Normal polipasto;
    enum CinematicPhases
    {
        scene_1,
        scene_2,
        scene_3,
        normal
    }
    CinematicPhases cinematicScenes;

    public int volverEscena;
    private void Awake()
    {
        cinematicScenes = CinematicPhases.normal;
    }
    private void Update()
    {
        switch (cinematicScenes)
        {
            case CinematicPhases.scene_1:
                CheckPointController.numeroCheckPoint = 1;
                SaveScene.instancia.finalLoop = true;
                StatusGameobjectsVariables.statusGameobject.modificacion[indexModi].modificacion = true;
                timeToStartCinematic += Time.deltaTime;
                if (timeToStartCinematic >= 2) { AnimationHud.detectar_echar.SetBool("Transicion", true); cinematicScenes = CinematicPhases.scene_2;}

                break;

            case CinematicPhases.scene_2:
                timeToStartCinematic -= Time.deltaTime;
                if (timeToStartCinematic <= 1) { boxWithPlayer.transform.parent = UbiBoxPlatform.transform; boxWithPlayer.transform.position = UbiBoxPlatform.position; polipasto.enabled = true;  cameraFollowBox.position = ubiDirectCamera.position; }
                if (timeToStartCinematic <= 0) { AnimationHud.detectar_echar.SetBool("Transicion", false); timeToStartCinematic = 0; cinematicScenes = CinematicPhases.scene_3; }
                break;
            case CinematicPhases.scene_3:
                if (!polipasto.bajar) 
                {
                    EstructuraNiveles.nivel = volverEscena;
                    AnimationHud.detectar_echar.SetTrigger("Detectar_Echar"); cinematicScenes = CinematicPhases.normal; 
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) cinematicScenes = CinematicPhases.scene_1;
    }
}

