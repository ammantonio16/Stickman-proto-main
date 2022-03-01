using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadordeTiempo : MonoBehaviour
{
    [SerializeField]
    public static float tiempoAcabarTurno = 20f;
    public Text reloj;
    public void TiempoRestante()
    {
        tiempoAcabarTurno = tiempoAcabarTurno - Time.deltaTime;
        reloj.text = tiempoAcabarTurno.ToString("f0");
        if (tiempoAcabarTurno <= 0)
        {
            Turn.turnos = false;
            tiempoAcabarTurno = 20f;
            JugadorController.derecha = false;
            JugadorController.izquierda = false;
        }
    }
    public void TiempoRestanteEnemy()
    {
        tiempoAcabarTurno = tiempoAcabarTurno - Time.deltaTime;
        reloj.text = tiempoAcabarTurno.ToString("f0");
        if (tiempoAcabarTurno <= 0)
        {
            Turn.turnos = true;
            tiempoAcabarTurno = 20f;
        }
    }
}
