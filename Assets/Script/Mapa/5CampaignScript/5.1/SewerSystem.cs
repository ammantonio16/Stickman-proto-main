using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerSystem : MonoBehaviour
{
    public int indexMod;
    [SerializeField] GameObject aqua;
    [SerializeField] Camino_CambiarZona pozoArea;
    [SerializeField] Events evento;
    PlayerHere player;

    bool activeEvent;
    ActivarModificador activar;
    private void Awake()
    {
        activar = GetComponent<ActivarModificador>();
        player = GetComponent<PlayerHere>();
    }
    private void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) Drain();
    }

    // Update is called once per frame
    void Update()
    {
        //This active the event one time, no more
        if (!StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) ActionDrain();
        ExecuteDrain();
    }

    void ActionDrain()
    {
        //When you are here Drain() suscribe to events and on the switch
        if (player.playerImHere && activar.modificacion)
        {
            evento.actionOnCinematic += Drain;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                activeEvent = true;
            }
        }
        else evento.actionOnCinematic -= Drain;
    }

    void Drain()
    {
        //Make the action of event
        aqua.SetActive(false);
        pozoArea.enabled = true;
        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
        
    }
    void ExecuteDrain()
    {
        //Active Event when you press the Z
        if (activeEvent)
        {
            evento.Cinematic(ref activeEvent);
        }
    }
}
