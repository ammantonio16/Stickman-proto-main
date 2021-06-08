using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaloAntorchaActivador : MonoBehaviour
{
    //public GameObject paloAntorcha;
    public GameObject buttonInteract;
    private void Start()
    {
        //paloAntorcha.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Antorcha"))
        {
            buttonInteract.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("Player"))
        {
            buttonInteract.SetActive(false);
        }
    }


}
