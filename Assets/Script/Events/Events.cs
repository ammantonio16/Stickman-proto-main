using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void ActionCinematic();
    public event ActionCinematic actionOnCinematic;

    public BloquearMovimientoPlayer player;

    float timeChangeAction;
    public enum PhasesOfScene
    {
        black_screen,
        action,
        clear_screen,
        end
    }
    public PhasesOfScene phasesOnScene;
    public void Cinematic(ref bool finishCinematic)
    {
        
        switch (phasesOnScene)
        {
            case PhasesOfScene.black_screen:
                //Parar el movimiento
                player.BloquearMovPlayer();

                AnimationHud.detectar_echar.SetBool("Transicion", true);
                //StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
                phasesOnScene = PhasesOfScene.action;
                break;
            case PhasesOfScene.action:

                timeChangeAction += Time.deltaTime;
                if (timeChangeAction >= 2)
                {
                    actionOnCinematic();
                    phasesOnScene = PhasesOfScene.clear_screen;
                }
                break;
            case PhasesOfScene.clear_screen:
                player.HabilitarMovPlayer();
                AnimationHud.detectar_echar.SetBool("Transicion", false);
                phasesOnScene = PhasesOfScene.end;
                break;
            case PhasesOfScene.end:
                Debug.Log("End Of Scene");
                timeChangeAction = 0;
                finishCinematic = false;
                phasesOnScene = PhasesOfScene.black_screen;
                break;
        }

    } 

}
