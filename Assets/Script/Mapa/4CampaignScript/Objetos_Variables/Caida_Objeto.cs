using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caida_Objeto : MonoBehaviour
{

    public int indexModificador;

    [SerializeField] GameObject objetoInGround;

    [SerializeField] GameObject objetoCaido;

    private void Start()
    {
        ObjetoInGround();
    }
    public void ObjetoInGround()
    {
        //Cuando un objeto cae deja el objeto roto en el suelo
        if (StatusGameobjectsVariables.statusGameobject.modificacion[indexModificador].modificacion)
        {
            objetoInGround.SetActive(true);
            if(objetoCaido != null)
            {
                Destroy(objetoCaido);
            }
        }
    }
    
}
