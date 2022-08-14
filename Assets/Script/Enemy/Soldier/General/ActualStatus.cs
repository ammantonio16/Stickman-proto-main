using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualStatus : MonoBehaviour
{
    [Header("Identity")]
    public int iD;
    [Header("Vida Soldier UI")]
    [SerializeField] SpriteRenderer head;
    [SerializeField] List<Sprite> cascos;
    [HideInInspector] public int indexCasco;

    SoldierLife soldierLife;
    //Nota: For soldadosDetectar doesnt work, because you dont put her script
    SoldadoNormal soldadoMode;
    int oneTimeAttackMode;
    int oneTimeWarningMode;
    private void Awake()
    {
        soldierLife = GetComponent<SoldierLife>();
        soldadoMode = GetComponent<SoldadoNormal>();
        
    }
    void Start()
    {
        LoadStatus();
        EstablishLifeBegin();
        
    }
    private void Update()
    {
        LifeConstantly();

        if(ListaAttackSoldado.modeSoldiers.activeAllAttackSoldiers && oneTimeAttackMode < 1 && soldierLife.vida > 0)
        {
            ChangeModeAttackSoldier();
            oneTimeAttackMode++;
        }
        if(ListaAttackSoldado.modeSoldiers.warningAllSoldiers && !ListaAttackSoldado.modeSoldiers.activeAllAttackSoldiers && oneTimeWarningMode < 1)
        {
            //ISeeCadaver();
            oneTimeWarningMode++;
        }
        
    }
    private void OnDestroy()
    {
        SaveStatus();
        
    }

   
    void SaveStatus()
    {
        //Necesitas algo para identificar el array

        //Cuando Aparece Cae dormido Deberia aparecer ya dormido
        SoldierActiveInScene.instancia.soldiersStatus[iD].statusVida = soldierLife.vida;
        //Si con transform no funciona el almacenado, almacena los vectores
        SoldierActiveInScene.instancia.soldiersStatus[iD].position_x = transform.position.x;
        SoldierActiveInScene.instancia.soldiersStatus[iD].position_y = transform.position.y;
        //Mode From soldier
        SoldierActiveInScene.instancia.soldiersStatus[iD].modeSoldier = soldadoMode.berserker;
        SoldierActiveInScene.instancia.soldiersStatus[iD].warningSoldier = soldadoMode.seeCadaverWarning;
    }
    void LoadStatus()
    {
        soldierLife.vida = SoldierActiveInScene.instancia.soldiersStatus[iD].statusVida;
        soldadoMode.berserker = SoldierActiveInScene.instancia.soldiersStatus[iD].modeSoldier;
        soldadoMode.seeCadaverWarning = SoldierActiveInScene.instancia.soldiersStatus[iD].warningSoldier;
        if (Mathf.Abs(SoldierActiveInScene.instancia.soldiersStatus[iD].position_x) > 0 && Mathf.Abs(SoldierActiveInScene.instancia.soldiersStatus[iD].position_y) > 0)
        {
            //SoldierActiveInScene.instancia.soldiersStatus[0].lastPositionInScene.position = transform.position;
            Debug.Log("Me he transportado");

            Vector3 lastPosition;
            lastPosition = new Vector3(SoldierActiveInScene.instancia.soldiersStatus[iD].position_x, SoldierActiveInScene.instancia.soldiersStatus[iD].position_y + 1, 0f);

            transform.position = lastPosition;

        }
    }

    public void ChangeModeAttackSoldier()
    {
        //COlocar update
        soldadoMode.berserker = SoldierActiveInScene.instancia.soldiersStatus[iD].modeSoldier;
        soldadoMode.seeCadaverWarning = false;
    }
    public void ISeeCadaver()
    {
        Debug.Log("Chocolata");
        soldadoMode.seeCadaverWarning = SoldierActiveInScene.instancia.soldiersStatus[iD].warningSoldier;
    }
    void EstablishLifeBegin()
    {
        if (soldierLife.vida > 0) indexCasco = soldierLife.vida - 1;
        head.sprite = cascos[indexCasco];
    }
    void LifeConstantly()
    {
        if (indexCasco >= 0) head.sprite = cascos[indexCasco];
        else indexCasco = 0;

        if(soldierLife.vida <= 0)
        {
            head.sprite = cascos[0];
        }
    }
}
