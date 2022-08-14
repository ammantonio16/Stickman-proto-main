using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desenterrar : MonoBehaviour
{
    [Header("Modificador")]
    public int indexMod;
    [SerializeField] ActivarModificador activador;

    [Header("Item you Remove when Unearth")]
    [SerializeField] List<GameObject> itemEarth;
    [Header("Item that want Appear when Unearth")]
    [SerializeField] List<GameObject> itemDownEarth;

    [Header("Event Transition")]
    [SerializeField] Events evento;

    [Header("Detection ubi Player")]
    [SerializeField] PlayerHere playerHere;

    [Header("Action to DisAppear/Appear")]
    [SerializeField] Aparecer_DesAp_Obj ap_Des_Obj;

    bool beginEvent;



    private void Start()
    {
        
        //Tras hacer la accion una vez se guarda para no tener que comerte la "Cinematica"
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) RemoveEarth();
        
    }
    private void Update()
    {
        if (!StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) ActiveUnearth();
        ExecuteUnearth();
        //Una vez lo activas por primera vez hace toda la "Cinematica"
    }

    void ActiveUnearth()
    {
        if (playerHere.playerImHere)
        {
            evento.actionOnCinematic += RemoveEarth;
            evento.actionOnCinematic += Unearth;
            if (activador.modificacion)
            {
                beginEvent = true;
            }
        }
        else 
        {
            evento.actionOnCinematic -= RemoveEarth;
            evento.actionOnCinematic -= Unearth;
        }
        
    }

    void ExecuteUnearth()
    {
        if (beginEvent)
        {
            evento.Cinematic(ref beginEvent);
        }
    }

    void RemoveEarth()
    {
        ap_Des_Obj.DesaparacerObjetos(itemEarth);
        
        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
    }
    void Unearth()
    {
        ap_Des_Obj.AparecerObjetos(itemDownEarth);
        //StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
    }
}
