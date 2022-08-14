using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambiar_Area: MonoBehaviour
{

    public int siguienteEscena;
    public int checkPoint;
    public bool usarCheckpoint;
    bool entrarCamino;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            entrarCamino = true;

        }

    }
    private void Update()
    {
        if (entrarCamino) { CambiarNivel(); }
    }
    void CambiarNivel()
    {
        EstructuraNiveles.nivel = siguienteEscena;
        AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
        if (usarCheckpoint) CheckPointController.numeroCheckPoint = checkPoint;
    }
}
