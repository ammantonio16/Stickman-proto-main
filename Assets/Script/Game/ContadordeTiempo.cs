using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadordeTiempo : MonoBehaviour
{
    public float tiempoAcabarTurno;

    public Text reloj;
    public void TiempoRestante()
    {
        tiempoAcabarTurno = tiempoAcabarTurno - Time.deltaTime;
        reloj.text = tiempoAcabarTurno.ToString("f0");
        if (tiempoAcabarTurno <= 0)
        {
            Turn.turnos = false;
            tiempoAcabarTurno = 20f;
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
