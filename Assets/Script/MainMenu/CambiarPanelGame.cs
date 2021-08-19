using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarPanelGame : MonoBehaviour
{
    public GameObject panelGame;
    public Text tituloNivel;
    public Image imagenDelNivel;
    public Text descripcionDeNiveles;
    public Text[] titulosTodosNiveles;
    public Image[] imagenTodosNiveles;
    public Text[] descripcionTodosNiveles;

    void Start()
    {
        panelGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            tituloNivel.text = titulosTodosNiveles[1].text;
            imagenDelNivel.sprite = imagenTodosNiveles[1].sprite;
            descripcionDeNiveles.text = descripcionTodosNiveles[1].text;
        }
    }
}
