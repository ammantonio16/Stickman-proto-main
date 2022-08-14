using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDoor : MonoBehaviour
{
    public AudioSource musicAmbient;
    public SoundManager playerWalk;
    public AudioClip cave;
    public AudioClip forest;
    public bool play;
    void Start()
    {
        play = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //cambiar clip de Walk por el de gravilla
            Debug.Log("Payer;");
            play = !play;
            if (play)
            {
                playerWalk.controlAudio[1].clip = playerWalk.audiosPlayer[0];
                playerWalk.controlAudio[1].Play();
                playerWalk.controlAudio[1].volume = 0.2f;
                musicAmbient.clip = forest;
                musicAmbient.Play();

            }
            if (!play)
            {
                playerWalk.controlAudio[1].clip = playerWalk.audiosPlayer[4];
                playerWalk.controlAudio[1].volume = 0.5f;
                playerWalk.controlAudio[1].Play();
                musicAmbient.clip = cave;
                musicAmbient.Play();
            }
        }
    }
}
