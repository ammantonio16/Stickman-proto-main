using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aparecer_DesAp_Obj : MonoBehaviour
{
    public int indexMod;

    public enum AccionAp_DesAp
    {
        desaparecer,
        aparacer_Desaparecer,
        aparecer
    }
    [Header("Accion que quieres que se realice")]
    public AccionAp_DesAp cambiosEnEscena;

    [Header("Objetos que desaparecen")]
    [SerializeField] List<GameObject> objetosDesaparecer;
    [Header("Objetos que aparecen")]
    [SerializeField] List<GameObject> objetosAparecer;
    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) 
        {
            switch (cambiosEnEscena)
            {
                case AccionAp_DesAp.desaparecer:
                    DesaparacerObjetos(objetosDesaparecer);
                    break;
                case AccionAp_DesAp.aparacer_Desaparecer:
                    DesaparacerObjetos(objetosDesaparecer);
                    AparecerObjetos(objetosAparecer);
                    break;
                case AccionAp_DesAp.aparecer:
                    AparecerObjetos(objetosAparecer);
                    break;
            }

        }
       
    }

    public void DesaparacerObjetos(List<GameObject> objectWantDis)
    {
        //Hace desparacer ciertos objetos de escena, para simular que se han cambiado de sitio
        foreach(GameObject objeto in objectWantDis)
        {
            objeto.SetActive(false);
        }
    }
    public void AparecerObjetos(List<GameObject> objectAppear)
    {
        //Hace aparecer objetos en escena
        foreach (GameObject objeto in objectAppear)
        {
            objeto.SetActive(true);
        }
    }
}
