using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinemtic_AbrirPuerta : MonoBehaviour
{
    public int indexModificacion;

    [Header("Datos Cinematica")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject puerta;
    [SerializeField] Inventory inventario;

    [Header("Camera Objective")]
    [SerializeField] Transform puertaObjetivo;
    [SerializeField] Transform cameraPosition;

    [SerializeField] CinemachineVirtualCamera cV_Camera;
    enum CinematicTemplo
    {
        transicionInicial,
        escenaPuertaTemplo,
        transicionFinal,
        endCinematic
    }

    CinematicTemplo cinematic_Templo;

    ActivarModificador activarCinematic;

    float tiempoCambiarCinematic;

    private void Awake()
    {
        activarCinematic = GetComponent<ActivarModificador>();
    }
    void Update()
    {
        if (!StatusGameobjectsVariables.statusGameobject.modificacion[indexModificacion].modificacion) CinematicTemploDoor();
        else { puerta.transform.position = puertaObjetivo.position; }
    }
    void CinematicTemploDoor()
    {
        if (activarCinematic.modificacion)
        {
            
            switch (cinematic_Templo)
            {
                case CinematicTemplo.transicionInicial:
                    AnimationHud.detectar_echar.SetBool("Transicion", true);
                    tiempoCambiarCinematic += Time.deltaTime;
                    inventario.enabled = false;
                    if(tiempoCambiarCinematic >= 1.5f)
                    {
                        cinematic_Templo = CinematicTemplo.escenaPuertaTemplo;
                        cV_Camera.Follow = cameraPosition;
                        player.SetActive(false);
                    }
                    //cinematic_Templo = CinematicTemplo.escenaAbrirPuertaTemplo;
                    //Colocar tiempo
                    break;
                case CinematicTemplo.escenaPuertaTemplo:
                    AnimationHud.detectar_echar.SetBool("Transicion", false);
                    tiempoCambiarCinematic -= 0.01f;
                    if(tiempoCambiarCinematic <= 0)
                    {
                        puerta.transform.position = new Vector3(puerta.transform.position.x, puerta.transform.position.y + 0.01f, puerta.transform.position.z);
                        cV_Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
                        if (puerta.transform.position.y >= puertaObjetivo.position.y)
                        {
                            cinematic_Templo = CinematicTemplo.transicionFinal;
                        }
                    }
                    //Colocar tiempo
                    break;
                case CinematicTemplo.transicionFinal:
                    cV_Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                    AnimationHud.detectar_echar.SetBool("Transicion", true);
                    tiempoCambiarCinematic += Time.deltaTime;
                    if(tiempoCambiarCinematic >= 1.5f)
                    {
                        cV_Camera.Follow = player.transform;
                        player.SetActive(true);
                        cinematic_Templo = CinematicTemplo.endCinematic;
                    }
                    break;
                case CinematicTemplo.endCinematic:
                    AnimationHud.detectar_echar.SetBool("Transicion", false);
                    inventario.enabled = true;
                    StatusGameobjectsVariables.statusGameobject.modificacion[indexModificacion].modificacion = true;
                    break;

            }
        }
    }
}
