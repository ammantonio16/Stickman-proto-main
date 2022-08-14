using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Player")]
    public AudioClip[] audiosPlayer;
    [Header("Ambiente")]
    public AudioClip[] audiosAmbiente;
    [Header("Efectos Armas")]
    public AudioClip[] audiosArmas;

    public  AudioSource[] controlAudio;
    private void Awake()
    {
        controlAudio[0] = GetComponent<AudioSource>();
    }
    private void Start()
    {
        
    }

    /// <summary>
    /// Reproducir un audio una vez
    /// </summary>
    /// <param name="audios">La lista donde esta audio que quieras reproducir</param>
    /// <param name="indice">En que posicion de la lista esta el audio</param>
    /// <param name="volumen">Volumen</param>
    /// <param name="indexControl">Seleccionar la fuente de sonido donde quieres que se reproduzca</param>
    public void SeleccionAudio(AudioClip[] audios, int indice, float volumen, int indexControl)
    {
        controlAudio[indexControl].PlayOneShot(audios[indice], volumen);
        //Debug.Log(audios[indice].length);
        
    }
}
