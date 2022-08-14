using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegretScene : MonoBehaviour
{
    public int indexMod;
    public int spawn;
    public int nextScene;
    bool regret;
    void Update()
    {
        GetBack();
    }
    void GetBack()
    {
        if (regret)
        {
            CheckPointController.numeroCheckPoint = spawn;
            EstructuraNiveles.nivel = nextScene;
            AnimationHud.detectar_echar.SetTrigger("Detectar_Echar");
            StatusGameobjectsVariables.statusGameobject.modificacion[indexMod].modificacion = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) regret = true;
    }
}
