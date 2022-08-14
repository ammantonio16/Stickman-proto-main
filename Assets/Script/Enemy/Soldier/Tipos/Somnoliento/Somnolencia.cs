using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Somnolencia : MonoBehaviour
{
    [SerializeField]ParticleSystem particulasDormido;
    SoldadoNormal soldado;

    [Header ("Time Sleep")]
    public float tiempoLimiteSomnoliento;
    public float tiempoLimiteDespierto;
    float tiempoSomnoliento;
    float tiempoDespierto;

    [Header("ChangePatrol")]
    public List<Transform> ubis;
    

    bool despierto;

    private void Awake()
    {
        soldado = GetComponent<SoldadoNormal>();
        despierto = true;
    }
    
    void Update()
    {
        if (!soldado.berserker && !soldado.busquedaMode) Somnoliento();
        else if(soldado.berserker)
        {
            ChangePatrolMov();
            this.enabled = false;
            soldado.enabled = true;
            particulasDormido.Stop(); 
        }
    }

    void Somnoliento()
    {
        //Despierto
        if (despierto) {tiempoDespierto += Time.deltaTime; tiempoSomnoliento = 0; }
        //Dormido
        else { tiempoSomnoliento += Time.deltaTime; tiempoDespierto = 0; }

        if (tiempoDespierto >= tiempoLimiteDespierto) { particulasDormido.Play(); soldado.enabled = false; despierto = false; }


        if (tiempoSomnoliento >= tiempoLimiteSomnoliento) { particulasDormido.Stop(); soldado.enabled = true; despierto = true; }
    }
    void ChangePatrolMov()
    {
        soldado.ubicacionesDirigir.Clear();

        for (int i = 0; i < ubis.Count; i++)
        {
            soldado.ubicacionesDirigir.Add(ubis[i]);
        }
    }
}
