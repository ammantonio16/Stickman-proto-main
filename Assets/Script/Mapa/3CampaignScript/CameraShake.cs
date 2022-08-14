using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera cineMachineCamera;

    public int zonaModificada;
    private void Awake()
    {
        //cineMachineCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void Shake()
    {
        
        CinemachineBasicMultiChannelPerlin cineMachinePerlin = cineMachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cineMachinePerlin.m_AmplitudeGain = 1f;

        EstructuraNiveles.nivel = zonaModificada;
    }
}
