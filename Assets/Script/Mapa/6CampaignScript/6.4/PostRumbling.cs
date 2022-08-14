using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRumbling : MonoBehaviour
{
    public int indexModRumbling;
    public int sceneChange;

    [SerializeField] Camino_CambiarZona restRoom;
    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexModRumbling].modificacion) restRoom.siguienteEscena = sceneChange;
    }
}
