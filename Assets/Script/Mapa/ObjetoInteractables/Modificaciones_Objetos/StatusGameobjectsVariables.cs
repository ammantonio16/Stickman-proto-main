using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGameobjectsVariables : MonoBehaviour
{
    //Esto almacenara el estado de un objeto, haciendo que un objeto sea guardado si ha sido modificado por el Player
    public static StatusGameobjectsVariables statusGameobject;


    public List<ListaModificaciones> modificacion;





    private void Awake()
    {


        if (StatusGameobjectsVariables.statusGameobject == null)
        {
            StatusGameobjectsVariables.statusGameobject = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }
}
