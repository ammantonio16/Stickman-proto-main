using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopFinish : MonoBehaviour
{
    [Header("Loop Data")]
    public int loopRepeat;
    public int loopRepeatLimit;
    [Header("Scene Data")]
    public int siguienteScena;

    bool end;
    [Header("Modificaciones Save")]
    public int indexMod;
    [Header("Checkpoint Data")]
    public int checkpoint;

    // Update is called once per frame
    void Update()
    {
        Finish_Loop();
    }
    void Finish_Loop()
    {
        if(loopRepeat > loopRepeatLimit)
        {
            CheckPointController.numeroCheckPoint = checkpoint;
            EstructuraNiveles.nivel = siguienteScena;
            AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
            end = true;
        } 
    }
    private void OnDestroy()
    {
        SaveStatusLoop();
        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
    }
    void SaveStatusLoop() 
    {
        SaveScene.instancia.finalLoop = end;
    }
    
}
