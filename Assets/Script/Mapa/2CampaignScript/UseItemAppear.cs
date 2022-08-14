using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemAppear : MonoBehaviour
{
    ActivarModificador activador;
    public int indexMod;

    [SerializeField] Aparecer_DesAp_Obj appe_des;
    [SerializeField] List<GameObject> appear;
    private void Awake()
    {
        activador = GetComponent<ActivarModificador>();
    }
    void Start()
    {
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion) 
        {
            appe_des.AparecerObjetos(appear);
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activador.modificacion) 
        {
            appe_des.AparecerObjetos(appear);
            StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
        }
    }

}
