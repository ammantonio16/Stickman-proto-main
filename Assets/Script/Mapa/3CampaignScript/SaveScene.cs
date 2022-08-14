using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScene : MonoBehaviour
{
    public static SaveScene instancia;



    #region ListaItemsNivel
    public List<ListaTodosObjetos> listaItemsNivelGuardar;
    #endregion

    #region EndLoop(Nivel_4)
    [HideInInspector] public bool finalLoop;
    #endregion

    //Probar sei estos datos al cambiar se mantionen o no
    #region ViajeRapido(Nivel_ 4)
    [HideInInspector] public int usosViajeRapido = 1;
    #endregion

    #region Templo(Nivel_4)
    [HideInInspector] public bool activarTrampasTemplo;
    #endregion

    #region TheLastSoldiersFloor0(Nivel_6.3)
    [HideInInspector] public float xLastDeath;
    [HideInInspector] public float yLastDeath;
    [HideInInspector] public int onlyOne;
    #endregion

    #region B8(Best Weapon)(6.5)
    public bool b8;
    [HideInInspector] public int b8Reogida;
    [HideInInspector] public bool izqEscombrosB8;
    //condiciones para que aparezca la bola rota:
    //1 tener la bola, 2 usar en el rumbling
    //no aparicion:
    //1 bo haber recogido la bola, 2 no haber usado la bola, 3
    //Es inversamente proporcional, si yo ya no tengo el objeto es porque lo he usado y se ha roto; si sigo teniendo el objeto es porque he usado municion. teniendo en cuenta que previamente la recogida es +1
    #endregion

    //Quiero que ocurra en contadas escenas
    private void Awake()
    {

        if (SaveScene.instancia == null)
        {
            SaveScene.instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClearListIteams()
    {
        for(int i = 0; i < listaItemsNivelGuardar.Count; i++)
        {
            listaItemsNivelGuardar[i].objetoObtenido = false;
            listaItemsNivelGuardar[i].ubiPosButton = 0;
            listaItemsNivelGuardar[i].vecesUsadoObjeto = 0;
        }
    }






}
