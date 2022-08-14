using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derrumbamiento : MonoBehaviour
{
    [Header("Modificacion")]
    public int indexMod;
    //level 3.2 Poner la lista de modificaciones

    [Header("Next Scene")]
    public int nextLevel;
    public int checkpoint;

    [Header("Player Mov Stop")]
    [SerializeField] BloquearMovimientoPlayer player;

    [Header("Soldier Inactive")]
    public List<SoldadoNormal> listaSoldadosCopia;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaPlayer")) 
        {    
            Destroy(collision.gameObject);
            Rumbling();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("B8"))
        {
            
            Rumbling();
        }
    }
    void Rumbling()
    {
        StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;

        //Asign the level
        EstructuraNiveles.nivel = nextLevel;
        CheckPointController.numeroCheckPoint = checkpoint;

        //Comienza el <<Rumbling>>
        AnimationHud.detectar_echar.SetBool("Derrumbar", true);

        //Bloquea los movimientos de los enemigos, para no ser atacado
        foreach (SoldadoNormal soldado in listaSoldadosCopia)
        {

            soldado.enabled = false;
            soldado.GetComponent<Animator>().SetBool("WalkSoldier", false);

        }

        //Bloquea tus movimientos y arma para no aprovechar la situacion
        player.BloquearMovPlayer();
    }
    
}
