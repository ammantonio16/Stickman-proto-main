using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarCuerda : MonoBehaviour
{
    [Tooltip("Identity is number of modification")]
    public int identity;
    ActivarModificador activar;
    [SerializeField] GameObject cuerda;

    private void Awake()
    {
        activar = GetComponent<ActivarModificador>();
    }
    void Start()
    {
        LoadEstado();
    }

    void Update()
    {
        //Simula que colocas un objeto,activando uno ya presente en escena
        if (activar.modificacion) { cuerda.SetActive(true); }
    }
    private void OnDestroy()
    {
        Modificacion.SaveEstado(identity, activar.modificacion);
    }
    public void LoadEstado()
    {
        //no Coge el bool

        activar.modificacion = StatusGameobjectsVariables.statusGameobject.modificacion[identity].modificacion;
    }
}
