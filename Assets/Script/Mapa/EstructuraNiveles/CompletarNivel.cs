using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletarNivel : MonoBehaviour
{
    public int siguienteNivel;
    [SerializeField] BloquearMovimientoPlayer blockPlayer;
    EstructuraNiveles anim = new EstructuraNiveles();

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //NOTA:
            //En cuyo caso de querer hacer una pantalla de carga seria:
            //1.Este script te dirige a la pantalla de carga con el número <<X>> y sigue poniendo <<siguienteNivel>> en EstructurarNivel;
            //2.En la pantalla de carga te pondrá información del siguiente nivel para leer y cuando toques una tecla te cargar el siguiente.
            //3.Llama a SceneManager directamente, poruqe como siempre es la misma pantalla de carga, solo hay que cambiar los textos.
            blockPlayer.BloquearMovPlayer();
            EstructuraNiveles.nivel = siguienteNivel;
            AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
            if(SaveScene.instancia != null)
            {
                SaveScene.instancia.ClearListIteams();
            }
            //if (TotalTerminales.terminalesTotales != null) TotalTerminales.terminalesTotales.PortatilValueReset();
            CheckPointController.numeroCheckPoint = 0;
            Destroy(SoldierActiveInScene.instancia.gameObject);
            Destroy(StatusGameobjectsVariables.statusGameobject.gameObject);
            Destroy(TotalTerminales.terminalesTotales.gameObject);
            Destroy(ListaAttackSoldado.modeSoldiers.gameObject);
        }

    }
}
