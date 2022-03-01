using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDBotones : MonoBehaviour
{
    [Header("SOLO SE RELLENA EN EL MapaConNiveles")]
    //Mirar explicacion de su utilidad en el script "NivelesMapa" apartado "OnTriggerEnter2D"
    public int iD;
    [Header("SOLO SE RELLENA EN LAS CAMPAÑAS")]
    public int ubicacion;

    public static int ubicacionNivel;
    void Update()
    {
      /*Se hace esto porque se necesitaba de una variable estática para conectarla con los otros script,
        pero a la vez pública para asignar los niveles con las ubicaciones en el mapa.*/
        ubicacionNivel = ubicacion;
    }
}
