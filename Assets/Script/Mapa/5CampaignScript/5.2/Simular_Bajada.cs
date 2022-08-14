using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simular_Bajada : MonoBehaviour
{
    [SerializeField] BloquearMovimientoPlayer player;
    [SerializeField] List<SpriteRenderer> spritesPlayer;
    

    void Detras()
    {
        player.BloquearMovPlayer();
        foreach(SpriteRenderer sprite in spritesPlayer)
        {
            sprite.sortingOrder = -100;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) Detras();
    }
}
