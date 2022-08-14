using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierThatMove : MonoBehaviour
{
    [SerializeField] Transform spawnStart;
    [SerializeField] Transform soldadoMove;
    [SerializeField] SoldierLife soldadoMoveLife;

    int oneTime;

    private void Update()
    {
        if(oneTime < 1)
        {
            //This is the only character that i want preserve the same spawn when you enter in scene, unless this "died"
            //The main reason: Gameplay and not broken the scene
            if (soldadoMoveLife.vida > 0) soldadoMove.position = spawnStart.position;
            oneTime++;
        }
    }
}
