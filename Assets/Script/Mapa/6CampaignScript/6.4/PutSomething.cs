using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutSomething : MonoBehaviour
{
    ActivarModificador activador;
    public int indexMod;
    [SerializeField] Aparecer_DesAp_Obj desAp_Obj;

    [SerializeField] List<GameObject> wantAppear;
    [SerializeField] List<GameObject> wantDisappear;

    int once;
    private void Awake()
    {
        activador = GetComponent<ActivarModificador>();
    }
    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) PutItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (activador.modificacion && once < 1)
        {
            PutItem();
            once++;
        }
    }
    void PutItem()
    {
        desAp_Obj.AparecerObjetos(wantAppear);
        desAp_Obj.DesaparacerObjetos(wantDisappear);

        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
    }
}
