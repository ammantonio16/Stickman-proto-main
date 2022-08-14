using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    Transform player;

    public Transform destino;

    bool ActiveRoad;

    float timeToTeleport;

    LoopFinish loopEnd;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        loopEnd = FindObjectOfType<LoopFinish>();
    }


    // Update is called once per frame
    void Update()
    {
        Road();
    }

    void Road()
    {
        if (ActiveRoad && loopEnd.loopRepeat <= 5)
        {
            timeToTeleport += Time.deltaTime;
            AnimationHud.detectar_echar.SetBool("Transicion", true);
            if (timeToTeleport >= 2) { player.position = destino.position; ActiveRoad = false; }

        }
        else if (timeToTeleport >= 2) { AnimationHud.detectar_echar.SetBool("Transicion", false); timeToTeleport = 0; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { ActiveRoad = true; loopEnd.loopRepeat++; }

    }
}
